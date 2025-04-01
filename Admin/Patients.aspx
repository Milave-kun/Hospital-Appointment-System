<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Patients.aspx.vb" Inherits="Hospital_Appointment_System.Patients" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Patients - Dashboard</title>
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

        .header {
            position: relative;
            width: 100%;
            height: 50px; /* Adjust as needed */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="sidebar">
            <h4 id="selectedUsername" runat="server"></h4>
            <p>
                <asp:Literal ID="selectedRole" runat="server"></asp:Literal>
            </p>
            <asp:Button class="btn w-100" ID="logoutBtn" Text="LOG OUT" runat="server" OnClick="logoutBtn_Click" />
            <hr>
            <a class="sidebarr" href="Admin Dashboard.aspx"><i class="bi bi-house-door-fill"></i>Dashboard</a>
            <a class="sidebarr" href="Add doctor.aspx"><i class="bi bi-people-fill"></i>Doctors</a>
            <!-- <a class="sidebarr" href="Schedule Session.aspx"><i class="bi bi-calendar-fill"></i>Schedule</a> -->
            <a class="sidebarr" href="Manage Appointments.aspx"><i class="bi bi-file-medical-fill"></i>Appointment</a>
            <a class="sidebarr" href="Patients.aspx"><i class="bi bi-person-wheelchair"></i>Patients</a>
        </div>
        <div class="main-content">
            <div class="d-flex justify-content-between align-items-center mb-3">

                <!-- Search Box with Button -->
                <div style="max-width: 350px; width: 100%;">
                    <div class="input-group">
                        <asp:TextBox ID="searchTxt" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                        <asp:Button ID="searchBtn" runat="server" CssClass="btn btn-dark" Text="Search" />
                    </div>
                </div>

                <!-- Date Section -->
                <div class="d-flex align-items-center gap-2">
                    <span class="text-secondary small">Today's Date</span>
                    <strong class="fs-6">2025-02-06</strong>
                    <i class="bi bi-calendar-fill"></i>
                </div>
            </div>

            <div class="d-flex justify-content-between align-items-center mb-3">
                <!-- Search Box with Button -->
                <div style="max-width: 350px; width: 100%;">
                    <h4 style="font-weight: bold;">MANAGE PATIENTS</h4>
                    <p>All Patients <span id="patientsTotal" runat="server">(0)</span></p>
                </div>

                <!-- Date Section -->
            </div>

            <div class="table-responsive">
                <asp:Table  class="mt-1 table table-bordered" ID="Table1" runat="server"></asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
