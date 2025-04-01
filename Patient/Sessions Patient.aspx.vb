Imports System.Data.SqlClient

Public Class Sessions_Patient
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            appointmentID()
            Doctors()

            ' Check if user is logged in
            If Session("username") IsNot Nothing AndAlso Session("role") IsNot Nothing Then
                selectedUsername.InnerText = Session("username").ToString()
                selectedRole.Text = Session("role").ToString() ' Assign role to Literal control
            Else
                ' Redirect to login if session is empty
                Response.Redirect("~/Login.aspx")
            End If
        End If
        SessionsTbl()
    End Sub
    Private Sub appointmentID()
        Dim random As New Random()
        Dim appointmentID As String = random.Next(100000, 999999) ' Generates a random 6-digit number
        txtSessionID.Text = appointmentID ' Assuming you have a TextBox to display the ID
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
    Public Sub Doctors()
        Using conn As New SqlConnection(ConnectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT Doctor_ID, Doctor_Name FROM DoctorTbl WHERE Availability = 'Active'"
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

    Public Sub SessionsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "SESSION NUMBER"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "SESSION TITLE"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DATE"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "TIME"})
        Table1.Rows.Add(headerRow)

        ' Database connection and query execution
        Dim query As String = "SELECT * FROM SessionsTbl"
        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Session_Number").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Session_Title").ToString()})
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
        Sessions()
    End Sub
    Private Sub Sessions()
        Dim SessionID As Integer
        If Not Integer.TryParse(txtSessionID.Text.Trim(), SessionID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Appointment ID!', 'error');", True)
            Exit Sub
        End If

        Dim SessionTitle As String = txtSessionTitle.Text.Trim()
        Dim DoctorName As String = ddlDoctors.SelectedItem.Text
        Dim Datee As String = txtDate.Text.Trim()
        Dim Time As String = txtTime.Text.Trim()


        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(SessionTitle) OrElse String.IsNullOrEmpty(Datee) OrElse String.IsNullOrEmpty(Time) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO SessionsTbl (Session_Number, Session_Title, Doctor_Name, Date, Time) 
                             VALUES (@Session_Number, @Session_Title, @Doctor_Name, @Date, @Time)"
            Using cmd As New SqlCommand(sql, con)
                ' Correct parameter values
                cmd.Parameters.Add("@Session_Number", SqlDbType.Int).Value = SessionID
                cmd.Parameters.Add("@Session_Title", SqlDbType.VarChar, 100).Value = SessionTitle
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
                    SessionsTbl()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Failed to Register!', 'error');", True)
                End If
            End Using
        End Using
    End Sub
    Private Sub ClearTextboxes()
        txtSessionID.Text = ""
        txtSessionTitle.Text = ""
        ddlDoctors.SelectedIndex = 0
        txtDate.Text = ""
        txtTime.Text = ""
    End Sub
End Class