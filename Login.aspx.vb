Imports System.Data.SqlClient
Public Class Login
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Login()
    End Sub

    Private Sub Login()
        Dim username As String = usernameTxt.Text
        Dim password As String = passwordTxt.Text

        Using con As New SqlConnection(ConnectionString)
            Try
                con.Open()

                ' Query to check AdminTbl
                Dim adminQuery As String = "SELECT 'admin' AS role FROM AdminTbl WHERE username = @username AND password = @password"
                Dim doctorQuery As String = "SELECT 'doctor' AS role FROM DoctorTbl WHERE username = @username AND password = @password"
                Dim patientQuery As String = "SELECT 'patient' AS role FROM accounts WHERE username = @username AND password = @password"

                Dim userRole As String = ""

                Using cmd As New SqlCommand(adminQuery, con)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.HasRows Then
                        reader.Read()
                        userRole = reader("role").ToString()
                    End If
                    reader.Close()
                End Using

                ' If not found in AdminTbl, check DoctorTbl
                If userRole = "" Then
                    Using cmd As New SqlCommand(doctorQuery, con)
                        cmd.Parameters.AddWithValue("@username", username)
                        cmd.Parameters.AddWithValue("@password", password)
                        Dim reader As SqlDataReader = cmd.ExecuteReader()

                        If reader.HasRows Then
                            reader.Read()
                            userRole = reader("role").ToString()
                        End If
                        reader.Close()
                    End Using
                End If

                ' If not found in DoctorTbl, check accounts (Patients)
                If userRole = "" Then
                    Using cmd As New SqlCommand(patientQuery, con)
                        cmd.Parameters.AddWithValue("@username", username)
                        cmd.Parameters.AddWithValue("@password", password)
                        Dim reader As SqlDataReader = cmd.ExecuteReader()

                        If reader.HasRows Then
                            reader.Read()
                            userRole = reader("role").ToString()
                        End If
                        reader.Close()
                    End Using
                End If

                ' If userRole is assigned, login is successful
                If userRole <> "" Then
                    ' Store session
                    Session("username") = username
                    Session("role") = userRole

                    Dim redirectUrl As String = ""

                    ' Redirect based on user role
                    Select Case userRole.ToLower()
                        Case "admin"
                            redirectUrl = "/Admin/Admin Dashboard.aspx"
                        Case "doctor"
                            redirectUrl = "/Doctor/Doctor Dashboard.aspx"
                        Case "patient"
                            redirectUrl = "/Patient/Patient Dashboard.aspx"
                        Case Else
                            redirectUrl = "/Login.aspx"
                    End Select

                    ' Show success message and redirect
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage",
                        "Swal.fire('Success!', 'Login Successful!', 'success').then(() => { window.location='" & redirectUrl & "'; });", True)
                Else
                    ' Login failed
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage",
                        "Swal.fire('Error!', 'Invalid username or password!', 'error');", True)
                End If
            Catch ex As Exception
                ' Show database error
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage",
                    "Swal.fire('Error!', '" & ex.Message.Replace("'", "\'") & "', 'error');", True)
            End Try
        End Using

    End Sub
End Class