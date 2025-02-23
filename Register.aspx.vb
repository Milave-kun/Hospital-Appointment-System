Imports System.Data.SqlClient

Public Class Register
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        Register()
    End Sub

    Private Sub Register()
        ' Get user inputs and trim spaces
        Dim fullname As String = FullnameTxt.Text.Trim()
        Dim username As String = UsernameTxt.Text.Trim()
        Dim password As String = PasswordTxt.Text.Trim()
        Dim confirmPassword As String = confirmPassTxt.Text.Trim()

        ' Validation: Check if fields are empty
        If String.IsNullOrEmpty(fullname) OrElse String.IsNullOrEmpty(username) OrElse
       String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(confirmPassword) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        ' Validation: Check if password and confirm password match
        If password <> confirmPassword Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Passwords do not match!', 'error');", True)
            Exit Sub
        End If

        ' Validation: Check password strength (at least 8 chars, 1 uppercase, 1 number)
        'Dim passwordPattern As String = "^(?=.*[A-Z])(?=.*\d).{8,}$"
        'If Not Regex.IsMatch(password, passwordPattern) Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Password must be at least 8 characters long, contain at least one uppercase letter, and one number!', 'error');", True)
        '    Exit Sub
        'End If

        Using con As New SqlConnection(ConnectionString)
            con.Open()

            ' Validation: Check if username already exists
            Dim checkSql As String = "SELECT COUNT(*) FROM accounts WHERE Username = @Username"
            Using checkCmd As New SqlCommand(checkSql, con)
                checkCmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = username
                Dim userExists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                If userExists > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Username already exists!', 'error');", True)
                    Exit Sub
                End If
            End Using

            ' Insert new account if username is unique
            Dim sql As String = "INSERT INTO accounts (FullName, Username, Password) VALUES (@FullName, @Username, @Password)"
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = fullname
                cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = username
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = password ' Consider hashing before storing

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Successfully Registered!', 'success');", True)
                    ClearTextboxes()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Failed to Register!', 'error');", True)
                End If
            End Using
        End Using
    End Sub

    Public Sub ClearTextboxes()
        FullnameTxt.Text = ""
        UsernameTxt.Text = ""
        PasswordTxt.Text = ""
        confirmPassTxt.Text = ""
    End Sub
End Class