Imports System.Data.SqlClient

Public Class Appointments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AppointmentsTbl()
        appointmentID()
        Patients()

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

    Public Sub AppointmentsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.CssClass = "table-dark"
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT NUMBER"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "APPOINTMENT TITLE"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PATIENT NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "DATE"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "TIME"})
        Table1.Rows.Add(headerRow)
    End Sub

    Protected Sub searchBtn_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub saveBtn_Click(sender As Object, e As EventArgs)

    End Sub
End Class