Imports System.Data.SqlClient

Public Class Patient_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoctorsCount()
        TotalAppointment()
        AppointmentsTbl()

        If Not IsPostBack Then
            ' Check if user is logged in
            If Session("username") IsNot Nothing AndAlso Session("role") IsNot Nothing Then
                selectedUsername.InnerText = Session("username").ToString()
                selectedRole.Text = Session("role").ToString() ' Assign role to Literal control
            Else
                ' Redirect to login if session is empty
                Response.Redirect("~/Login.aspx")
            End If

            ' Check if FullName exists in session before using it
            If Session("FullName") IsNot Nothing Then
                usernamee.InnerText = Session("FullName").ToString()
            Else
                usernamee.InnerText = Session("username").ToString() ' Fallback to username if FullName is missing
            End If
        End If

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
    Public Sub DoctorsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM DoctorTbl", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    doctorsTotal.InnerText = Count.ToString()
                End Using
            End Using
        Catch ex As Exception
            doctorsTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub
    'Public Sub TodaysSession()
    '    Try
    '        Using Connection As New SqlConnection(ConnectionString)
    '            Connection.Open()
    '            Using Command As New SqlCommand("SELECT COUNT(*) FROM SessionsTbl", Connection)
    '                Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
    '                sessionTotal.InnerText = Count.ToString()
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        sessionTotal.InnerText = "Error: " & ex.Message ' Debugging output
    '    End Try
    'End Sub
    Public Sub TotalAppointment()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM PatientsAppointment", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    appointmentTotal.InnerText = Count.ToString()
                End Using
            End Using
        Catch ex As Exception
            appointmentTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub

    Public Sub AppointmentsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "Appointment No."})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "Appointment Title"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "Doctor Name"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "Date"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "Time"})
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

    Protected Sub appointments_Click(sender As Object, e As EventArgs)
        Response.Redirect("Appointments Patient.aspx")
    End Sub
End Class