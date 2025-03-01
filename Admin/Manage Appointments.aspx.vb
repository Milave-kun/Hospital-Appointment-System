Imports System.Data.SqlClient

Public Class Manage_Appointments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Patients()
            appointmentID()
            Doctors()
        End If
        AppointmentsTbl()
        AppointmentsCount()
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

    Public Sub Patients()
        Using conn As New SqlConnection(ConnectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT ID, FullName FROM accounts"
                Using cmd As New SqlCommand(query, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ddlPatients.Items.Clear()
                        ddlPatients.Items.Add(New ListItem("Select Patient Name", "")) ' Default option

                        While reader.Read()
                            Dim item As New ListItem(reader("FullName").ToString(), reader("ID").ToString())
                            ddlPatients.Items.Add(item)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle error (log or show a message)
            End Try
        End Using
    End Sub

    Public Sub Doctors()
        Using conn As New SqlConnection(ConnectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT Doctor_ID, Doctor_Name FROM DoctorTbl"
                Using cmd As New SqlCommand(query, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ddlDoctors.Items.Clear()
                        ddlDoctors.Items.Add(New ListItem("Select Doctors Name", "")) ' Default option

                        While reader.Read()
                            Dim item As New ListItem(reader("Doctor_Name").ToString(), reader("Doctor_ID").ToString())
                            ddlDoctors.Items.Add(item)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle error (log or show a message)
            End Try
        End Using
    End Sub


    Protected Sub btnSaveAppointment_Click(sender As Object, e As EventArgs)
        Appointments()
    End Sub

    Private Sub Appointments()
        Dim AppointmentID As Integer
        If Not Integer.TryParse(txtAppointmentID.Text.Trim(), AppointmentID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Appointment ID!', 'error');", True)
            Exit Sub
        End If

        Dim DoctorName As String = ddlDoctors.SelectedItem.Text
        Dim PatientName As String = ddlPatients.SelectedItem.Text
        Dim Appointment As String = txtAppointment.Text.Trim()


        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(PatientName) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO AppointmentsTbl (Appointment_ID, Doctor_Name, Patient_Name, Appointment) 
                             VALUES (@Appointment_ID, @Doctor_Name, @Patient_Name, @Appointment)"
            Using cmd As New SqlCommand(sql, con)
                ' Correct parameter values
                cmd.Parameters.Add("@Appointment_ID", SqlDbType.Int).Value = AppointmentID
                cmd.Parameters.Add("@Doctor_Name", SqlDbType.VarChar, 100).Value = DoctorName
                cmd.Parameters.Add("@Patient_Name", SqlDbType.VarChar, 100).Value = PatientName
                cmd.Parameters.Add("@Appointment", SqlDbType.VarChar, 100).Value = Appointment

                ' Open connection before executing
                con.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                con.Close()

                ' Check if insertion was successful
                If rowsAffected > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Successfully Registered!', 'success');", True)
                    ClearTextboxes()
                    AppointmentsTbl()
                    AppointmentsCount()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Failed to Register!', 'error');", True)
                End If
            End Using
        End Using
    End Sub

    Private Sub ClearTextboxes()
        txtAppointmentID.Text = ""
        txtAppointment.Text = ""
    End Sub

    Public Sub AppointmentsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PATIENT NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT"})
        Table1.Rows.Add(headerRow)

        ' Database connection and query execution
        Dim query As String = "SELECT * FROM AppointmentsTbl"
        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Appointment_ID").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Doctor_Name").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Patient_Name").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Appointment").ToString()})
                        Table1.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub
    Public Sub AppointmentsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM AppointmentsTbl", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    totalAppointments.InnerText = $"({Count})" ' Display count in parentheses
                End Using
            End Using
        Catch ex As Exception
            totalAppointments.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub
End Class