<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appointments.aspx.vb" Inherits="Hospital_Appointment_System.Appointments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctors Dashboard - Appointment</title>
    <link href="/CSS/Dashboard.css" rel="stylesheet" />

    <link
        rel="shortcut icon"
        href="/hospital-appointment-system (1).png"
        type="image/x-icon" />
    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
        crossorigin="anonymous" />
    <script
        src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        .btn {
            background-color: #001F3F;
            color: white;
        }

            .btn:hover {
                background-color: #001F3F;
                color: white;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="sidebar">
            <h4>Test Doctor</h4>
            <p>@doctor123</p>
            <asp:Button class="btn w-100" ID="logoutBtn" Text="LOG OUT" runat="server" OnClick="logoutBtn_Click" />
            <hr>
            <a class="sidebarr" href="Doctor Dashboard.aspx"><i class="bi bi-house-door-fill"></i>Dashboard</a>
            <a class="sidebarr" href="Appointments.aspx"><i class="bi bi-file-medical-fill"></i>My Appointments</a>
            <a class="sidebarr" href="Sessions.aspx"><i class="bi bi-clock-fill"></i>My Sessions</a>
            <a class="sidebarr" href="Patients.aspx"><i class="bi bi-person-wheelchair"></i>My Patients</a>
        </div>

        <div class="main-content">
            <div class="d-flex justify-content-end align-items-center mb-3">
                <!-- Date Section -->
                <div class="d-flex align-items-center gap-2">
                    <span class="text-secondary small">Today's Date</span>
                    <strong class="fs-6">2025-02-06</strong>
                    <i class="bi bi-calendar-fill"></i>
                </div>
            </div>

            <h4 style="font-weight: bold;" class="mb-5">MY APPOINTMENTS</h4>
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-flex align-items-center gap-2">
                    <button class="btn">Add Appointment</button>
                </div>

                <div style="max-width: 350px; width: 100%;">
                    <div class="input-group">
                        <asp:TextBox ID="searchTxt" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                        <asp:Button ID="searchBtn" runat="server" CssClass="btn btn-dark" Text="Search" />
                    </div>
                </div>
            </div>

            <div class="table-responsive mt-5">
                <table class="table table-bordered text-dark">
                    <thead class="bg-secondary text-white">
                        <tr>
                            <th>APPOINTMENT NUMBER</th>
                            <th>APPOINTMENT NAME</th>
                            <th>PATIENT NAME</th>
                            <th>DATE</th>
                            <th>TIME</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="5" class="bg-light text-center py-5">
                                <em>No records found</em>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
