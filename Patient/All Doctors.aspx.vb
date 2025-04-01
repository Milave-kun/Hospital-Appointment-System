Imports System.Data.SqlClient

Public Class All_Doctors
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoctorsTbl()

        If Not IsPostBack Then
            ' Check if user is logged in
            If Session("username") IsNot Nothing AndAlso Session("role") IsNot Nothing Then
                selectedUsername.InnerText = Session("username").ToString()
                selectedRole.Text = Session("role").ToString() ' Assign role to Literal control
            Else
                ' Redirect to login if session is empty
                Response.Redirect("~/Login.aspx")
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
    Public Sub DoctorsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DOCTOR NAME"})
        'headerRow.Cells.Add(New TableHeaderCell() With {.Text = "USERNAME"})
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
                        'tableRow.Cells.Add(New TableCell() With {.Text = row("Username").ToString()})
                        'tableRow.Cells.Add(New TableCell() With {.Text = row("Password").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Email").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Specialties").ToString()})

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
End Class