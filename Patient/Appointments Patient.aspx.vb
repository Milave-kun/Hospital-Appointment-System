Imports System.Data.SqlClient

Public Class Appointments_Patient
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            appointmentID()
            Doctors()
            Professions()

            ' Check if user is logged in
            If Session("username") IsNot Nothing AndAlso Session("role") IsNot Nothing Then
                selectedUsername.InnerText = Session("username").ToString()
                selectedRole.Text = Session("role").ToString() ' Assign role to Literal control
            Else
                ' Redirect to login if session is empty
                Response.Redirect("~/Login.aspx")
            End If
        End If
        AppointmentsTbl()
    End Sub

    Private Sub appointmentID()
        Dim random As New Random()
        Dim appointmentID As String = random.Next(100000, 999999) ' Generates a random 6-digit number
        txtAppointmentID.Text = appointmentID ' Assuming you have a TextBox to display the ID
    End Sub

    Protected Sub logoutBtn_Click(sender As Object, e As EventArgs)
        ' Destroy session
        Session.Abandon()
        Session.Clear()

        Dim script As String = "Swal.fire({ " &
                           "title: 'Are you sure?', " &
                           "text: 'You will be logged out!', " &
                           "icon: 'warning', " &
                           "showCancelButton: true, " &
                           "confirmButtonColor: '#3085d6', " &
                           "cancelButtonColor: '#d33', " &
                           "confirmButtonText: 'Yes, log me out!' " &
                           "}).then((result) => { " &
                           "if (result.isConfirmed) { " &
                           "window.location = '../Login.aspx'; } " &
                           "});"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "logoutMessage", script, True)
    End Sub

    Public Sub Doctors(Optional ByVal profession As String = "")
        Using conn As New SqlConnection(ConnectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT Doctor_ID, Doctor_Name FROM DoctorTbl"

                ' Apply profession filter if one is selected
                If Not String.IsNullOrEmpty(profession) Then
                    query &= " WHERE Specialties = @Specialties"
                End If

                Using cmd As New SqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(profession) Then
                        cmd.Parameters.AddWithValue("@Specialties", profession)
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ddlDoctors.Items.Clear()
                        ddlDoctors.Items.Add(New ListItem("Select Doctor's Name", "")) ' Default option

                        While reader.Read()
                            ddlDoctors.Items.Add(New ListItem(reader("Doctor_Name").ToString(), reader("Doctor_ID").ToString()))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle error (log or display)
            End Try
        End Using
    End Sub

    Public Sub Professions()
        Using conn As New SqlConnection(ConnectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT DISTINCT Specialties FROM DoctorTbl"
                Using cmd As New SqlCommand(query, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ddlProfession.Items.Clear()
                        ddlProfession.Items.Add(New ListItem("Select Doctor's Profession", "")) ' Default option

                        While reader.Read()
                            ddlProfession.Items.Add(New ListItem(reader("Specialties").ToString(), reader("Specialties").ToString()))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle error (log or display)
            End Try
        End Using
    End Sub

    Public Sub AppointmentsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT NUMBER"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DATE"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "TIME"})
        Table1.Rows.Add(headerRow)

        ' Database connection and query execution
        Dim query As String = "SELECT * FROM PatientsAppointment"
        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Appointment_Number").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Appointment_Name").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Doctor_Name").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Date").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Time").ToString()})
                        Table1.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnSession_Click(sender As Object, e As EventArgs)
        Appointments()
    End Sub
    Private Sub Appointments()
        Dim SessionID As Integer
        If Not Integer.TryParse(txtAppointmentID.Text.Trim(), SessionID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Appointment ID!', 'error');", True)
            Exit Sub
        End If

        Dim SessionTitle As String = txtAppointmentTitle.Text.Trim()
        Dim DoctorProfession As String = ddlProfession.SelectedItem.Text
        Dim DoctorName As String = ddlDoctors.SelectedItem.Text
        Dim Datee As String = txtDate.Text.Trim()
        Dim Time As String = txtTime.Text.Trim()


        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(SessionTitle) OrElse String.IsNullOrEmpty(Datee) OrElse String.IsNullOrEmpty(Time) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO PatientsAppointment (Appointment_Number, Appointment_Name, Profession, Doctor_Name, Date, Time) 
                             VALUES (@Appointment_Number, @Appointment_Name, @Profession, @Doctor_Name, @Date, @Time)"
            Using cmd As New SqlCommand(sql, con)
                ' Correct parameter values
                cmd.Parameters.Add("@Appointment_Number", SqlDbType.Int).Value = SessionID
                cmd.Parameters.Add("@Appointment_Name", SqlDbType.VarChar, 100).Value = SessionTitle
                cmd.Parameters.Add("@Profession", SqlDbType.VarChar, 100).Value = DoctorProfession
                cmd.Parameters.Add("@Doctor_Name", SqlDbType.VarChar, 100).Value = DoctorName
                cmd.Parameters.Add("@Date", SqlDbType.VarChar, 100).Value = Datee
                cmd.Parameters.Add("@Time", SqlDbType.VarChar, 100).Value = Time

                ' Open connection before executing
                con.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                con.Close()

                ' Check if insertion was successful
                If rowsAffected > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Successfully Registered!', 'success');", True)
                    ClearTextboxes()
                    AppointmentsTbl()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Failed to Register!', 'error');", True)
                End If
            End Using
        End Using
    End Sub

    Private Sub ClearTextboxes()
        txtAppointmentID.Text = ""
        txtDate.Text = ""
        ddlDoctors.SelectedIndex = 0
        txtDate.Text = ""
        txtTime.Text = ""
    End Sub

    Protected Sub ddlProfession_SelectedIndexChanged(sender As Object, e As EventArgs)
        Doctors(ddlProfession.SelectedValue)
    End Sub
End Class