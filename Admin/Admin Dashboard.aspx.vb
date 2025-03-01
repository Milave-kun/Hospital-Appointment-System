Imports System.Data.SqlClient

Public Class Admin_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoctorsCount()
        PatientsCount()
        AppointmentsCount()
        Appointments()
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

    Public Sub PatientsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM accounts", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    patientsTotal.InnerText = Count.ToString()
                End Using
            End Using
        Catch ex As Exception
            patientsTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub

    Public Sub AppointmentsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM AppointmentsTbl", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    appointnmentsTotal.InnerText = Count.ToString()
                End Using
            End Using
        Catch ex As Exception
            appointnmentsTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub

    Public Sub Appointments()
        ' Clear existing rows
        AppointmentsTbl.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PATIENT NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT"})
        AppointmentsTbl.Rows.Add(headerRow)

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
                        AppointmentsTbl.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub
End Class