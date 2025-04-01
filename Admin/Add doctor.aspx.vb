Imports System.Data.SqlClient
Imports System.IO

Public Class Add_doctor
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DoctorsTbl()
            ' Check if user is logged in
            If Session("username") IsNot Nothing AndAlso Session("role") IsNot Nothing Then
                selectedUsername.InnerText = Session("username").ToString()
                selectedRole.Text = Session("role").ToString() ' Assign role to Literal control
            Else
                ' Redirect to login if session is empty
                Response.Redirect("~/Login.aspx")
            End If
        End If
        DoctorsCount()
        doctorID()
    End Sub
    Private Sub doctorID()
        Dim random As New Random()
        Dim appointmentID As String = random.Next(100000, 999999) ' Generates a random 6-digit number
        txtDoctorID.Text = appointmentID ' Assuming you have a TextBox to display the ID
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
        Dim DoctorID As Integer
        If Integer.TryParse(txtDoctorID.Text.Trim(), DoctorID) Then
            ' Check if doctor exists
            Using con As New SqlConnection(ConnectionString)
                con.Open()
                Dim checkQuery As String = "SELECT COUNT(*) FROM DoctorTbl WHERE Doctor_ID = @DoctorID"
                Using checkCmd As New SqlCommand(checkQuery, con)
                    checkCmd.Parameters.AddWithValue("@DoctorID", DoctorID)
                    Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If count > 0 Then
                        updateDoctor() ' Update if exists
                    Else
                        saveDoctor() ' Save new if doesn't exist
                    End If
                End Using
            End Using
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Doctor ID!', 'error');", True)
        End If
    End Sub

    Private Sub saveDoctor()
        Dim DoctorID As Integer
        If Not Integer.TryParse(txtDoctorID.Text.Trim(), DoctorID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Doctor ID!', 'error');", True)
            Exit Sub
        End If

        Dim DoctorName As String = txtDoctorName.Text.Trim()
        Dim Username As String = txtUsername.Text.Trim()
        Dim Password As String = txtPassword.Text.Trim()
        Dim Email As String = txtEmail.Text.Trim()
        Dim Specialties As String = txtSpecialization.Text.Trim()
        Dim Availability As String = ddlAvailable.SelectedValue

        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(Username) OrElse
       String.IsNullOrEmpty(Password) OrElse String.IsNullOrEmpty(Email) OrElse
       String.IsNullOrEmpty(Specialties) OrElse String.IsNullOrEmpty(Availability) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Dim query As String = "INSERT INTO DoctorTbl (Doctor_ID, Doctor_Name, Username, Password, Email, Specialties, Availability) 
                               VALUES (@DoctorID, @Name, @Username, @Password, @Email, @Specialties, @Availability)"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID)
                cmd.Parameters.AddWithValue("@Name", DoctorName)
                cmd.Parameters.AddWithValue("@Username", Username)
                cmd.Parameters.AddWithValue("@Password", Password)
                cmd.Parameters.AddWithValue("@Email", Email)
                cmd.Parameters.AddWithValue("@Specialties", Specialties)
                cmd.Parameters.AddWithValue("@Availability", Availability)

                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh table
        DoctorsTbl()
        ClearTextboxes()

        ' Show success message
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Doctor added successfully!', 'success');", True)
    End Sub
    Private Sub updateDoctor()
        Dim DoctorID As Integer
        If Not Integer.TryParse(txtDoctorID.Text.Trim(), DoctorID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'Invalid Doctor ID!', 'error');", True)
            Exit Sub
        End If

        Dim DoctorName As String = txtDoctorName.Text.Trim()
        Dim Username As String = txtUsername.Text.Trim()
        Dim Email As String = txtEmail.Text.Trim()
        Dim Specialties As String = txtSpecialization.Text.Trim()
        Dim Availability As String = ddlAvailable.SelectedValue

        ' Check for empty fields
        If String.IsNullOrEmpty(DoctorName) OrElse String.IsNullOrEmpty(Username) OrElse
       String.IsNullOrEmpty(Email) OrElse String.IsNullOrEmpty(Specialties) OrElse String.IsNullOrEmpty(Availability) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Error!', 'All fields are required!', 'error');", True)
            Exit Sub
        End If

        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Dim query As String = "UPDATE DoctorTbl SET Doctor_Name = @Name, Username = @Username, 
                               Email = @Email, Specialties = @Specialties, Availability = @Availability 
                               WHERE Doctor_ID = @DoctorID"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID)
                cmd.Parameters.AddWithValue("@Name", DoctorName)
                cmd.Parameters.AddWithValue("@Username", Username)
                cmd.Parameters.AddWithValue("@Email", Email)
                cmd.Parameters.AddWithValue("@Specialties", Specialties)
                cmd.Parameters.AddWithValue("@Availability", Availability)

                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh table
        DoctorsTbl()

        ' Show success message
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "Swal.fire('Success!', 'Doctor updated successfully!', 'success');", True)
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

    Public Sub DoctorsTbl(Optional ByVal searchText As String = "")
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "USERNAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "EMAIL"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PROFESSION"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "AVAILABILITY"})
        Table1.Rows.Add(headerRow)

        ' Base query
        Dim query As String = "SELECT Doctor_ID, Doctor_Name, Username, Email, Specialties, Availability FROM DoctorTbl"

        ' Add WHERE clause only if searchText is provided
        If Not String.IsNullOrEmpty(searchText) Then
            query &= " WHERE Doctor_Name LIKE @searchText"
        End If

        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                ' Properly handle the search parameter
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.Add("@searchText", SqlDbType.VarChar).Value = "%" & searchText & "%"
                End If

                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        Dim doctorId As String = row("Doctor_ID").ToString()

                        ' Doctor ID (NOT EDITABLE)
                        tableRow.Cells.Add(New TableCell() With {.Text = doctorId, .CssClass = "doctor-id"})

                        ' Editable Cells
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Doctor_Name").ToString(), .CssClass = "editable"})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Username").ToString(), .CssClass = "editable"})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Email").ToString(), .CssClass = "editable"})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Specialties").ToString(), .CssClass = "editable"})

                        ' Availability with color
                        Dim availabilityCell As New TableCell() With {.CssClass = "editable"}
                        Dim availabilityStatus As String = row("Availability").ToString().Trim().ToLower()

                        If availabilityStatus = "on duty" Then
                            availabilityCell.Text = "<span style='color: green; font-weight: bold;'>" & "On Duty" & "</span>"
                        ElseIf availabilityStatus = "off duty" Then
                            availabilityCell.Text = "<span style='color: red; font-weight: bold;'>" & "Off Duty" & "</span>"
                        ElseIf availabilityStatus = "occupied" Then
                            availabilityCell.Text = "<span style='color: orange; font-weight: bold;'>" & "Occupied" & "</span>"
                        Else
                            availabilityCell.Text = availabilityStatus ' Default text
                        End If

                        tableRow.Cells.Add(availabilityCell)
                        Table1.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub

    Protected Sub searchBtn_Click(sender As Object, e As EventArgs)
        Dim searchTerm As String = searchTxt.Text.Trim()

        ' If search box is empty, show all doctors
        If String.IsNullOrEmpty(searchTerm) Then
            DoctorsTbl() ' Reload all doctors
        Else
            DoctorsTbl(searchTerm) ' Load filtered results
        End If
    End Sub

    ' Helper function to map column index to database column name
    Private Function GetColumnNameByIndex(index As Integer) As String
        Dim columns As String() = {"Doctor_ID", "Doctor_Name", "Username", "Email", "Specialties", "Availability"}
        Return If(index >= 0 AndAlso index < columns.Length, columns(index), "")
    End Function
End Class