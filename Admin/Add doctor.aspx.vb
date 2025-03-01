Imports System.Data.SqlClient

Public Class Add_doctor
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoctorsTbl()
        DoctorsCount()
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

    Protected Sub btnSaveDoctor_Click(sender As Object, e As EventArgs)
        saveDoctor()
    End Sub

    Private Sub saveDoctor()
        Dim DoctorID As Integer
        If Not Integer.TryParse(txtDoctorID.Text.Trim(), DoctorID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Doctor ID!', 'error');", True)
            Exit Sub
        End If

        Dim DoctorName As String = txtDoctorName.Text.Trim()
        Dim Username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim Email As String = txtEmail.Text.Trim()
        Dim Specialties As String = txtSpecialization.Text.Trim()
        Dim Availability As String = ddlAvailable.SelectedValue

        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(Username) OrElse
       String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(Email) OrElse
       String.IsNullOrEmpty(Specialties) OrElse String.IsNullOrEmpty(Availability) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO DoctorTbl (Doctor_ID, Doctor_Name, Username, Password, Email, Specialties, Availability) 
                             VALUES (@Doctor_ID, @Doctor_Name, @Username, @Password, @Email, @Specialties, @Availability)"
            Using cmd As New SqlCommand(sql, con)
                ' Correct parameter values
                cmd.Parameters.Add("@Doctor_ID", SqlDbType.Int).Value = DoctorID
                cmd.Parameters.Add("@Doctor_Name", SqlDbType.VarChar, 100).Value = DoctorName
                cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = Username
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = password
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email
                cmd.Parameters.Add("@Specialties", SqlDbType.VarChar, 100).Value = Specialties
                cmd.Parameters.Add("@Availability", SqlDbType.VarChar, 100).Value = Availability

                ' Open connection before executing
                con.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                con.Close()

                ' Check if insertion was successful
                If rowsAffected > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Successfully Registered!', 'success');", True)
                    ClearTextboxes()
                    DoctorsTbl()
                    DoctorsCount()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Failed to Register!', 'error');", True)
                End If
            End Using
        End Using
    End Sub

    Private Sub ClearTextboxes()
        txtDoctorID.Text = ""
        txtDoctorName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtEmail.Text = ""
        txtSpecialization.Text = ""
        ddlAvailable.SelectedValue = 0
    End Sub

    Public Sub DoctorsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM DoctorTbl", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    doctorsTotal.InnerText = $"({Count})" ' Display count in parentheses
                End Using
            End Using
        Catch ex As Exception
            doctorsTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub

    Public Sub DoctorsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "USERNAME"})
        'headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PASSWORD"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "EMAIL"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "SPECIALTIES"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "AVAILABILITY"})
        Table1.Rows.Add(headerRow)

        ' Database connection and query execution
        Dim query As String = "SELECT Doctor_ID, Doctor_Name, Username, Email, Specialties, Availability FROM DoctorTbl"
        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Doctor_ID").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Doctor_Name").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Username").ToString()})
                        'tableRow.Cells.Add(New TableCell() With {.Text = row("Password").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Email").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Specialties").ToString()})

                        ' Handle Availability with color
                        Dim availabilityCell As New TableCell()
                        Dim availabilityStatus As String = row("Availability").ToString()

                        If availabilityStatus.ToLower() = "active" Then
                            availabilityCell.Text = "<span style='color: green; font-weight: bold;'>" & availabilityStatus & "</span>"
                        ElseIf availabilityStatus.ToLower() = "inactive" Then
                            availabilityCell.Text = "<span style='color: red; font-weight: bold;'>" & availabilityStatus & "</span>"
                        Else
                            availabilityCell.Text = availabilityStatus ' Default text without styling
                        End If

                        tableRow.Cells.Add(availabilityCell)
                        Table1.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub
End Class