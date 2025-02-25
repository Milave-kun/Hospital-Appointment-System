Imports System.Data.SqlClient

Public Class Admin_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoctorsCount()
        PatientsCount()
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
End Class