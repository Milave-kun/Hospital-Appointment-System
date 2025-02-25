Imports System.Data.SqlClient

Public Class Patients
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        patientsTbl()
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

    Public Sub patientsTbl()
        ' Clear existing rows
        Table1.Rows.Clear()

        ' Add table headers
        Dim headerRow As New TableHeaderRow()
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PATIENT ID"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "PATIENT NAME"})
        headerRow.Cells.Add(New TableHeaderCell() With {.Text = "USERNAME"})
        Table1.Rows.Add(headerRow)

        ' Database connection and query execution
        Dim query As String = "SELECT * FROM accounts"
        Using con As New SqlConnection(ConnectionString)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Populate table with data
                    For Each row As DataRow In dt.Rows
                        Dim tableRow As New TableRow()
                        tableRow.Cells.Add(New TableCell() With {.Text = row("ID").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("FullName").ToString()})
                        tableRow.Cells.Add(New TableCell() With {.Text = row("Username").ToString()})
                        Table1.Rows.Add(tableRow)
                    Next
                End Using
            End Using
        End Using
    End Sub

    Public Sub PatientsCount()
        Try
            Using Connection As New SqlConnection(ConnectionString)
                Connection.Open()
                Using Command As New SqlCommand("SELECT COUNT(*) FROM accounts", Connection)
                    Dim Count As Integer = Convert.ToInt32(Command.ExecuteScalar())
                    patientsTotal.InnerText = $"({Count})" ' Display count in parentheses
                End Using
            End Using
        Catch ex As Exception
            patientsTotal.InnerText = "Error: " & ex.Message ' Debugging output
        End Try
    End Sub
End Class