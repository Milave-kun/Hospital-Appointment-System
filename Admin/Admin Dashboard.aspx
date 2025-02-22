<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Admin Dashboard.aspx.vb" Inherits="Hospital_Appointment_System.Admin_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrator Dashboard</title>
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
    <div class="sidebar">
        <h4>Administrator</h4>
        <p>@admin123</p>
        <b class="btn w-100">LOG OUT</b>
        <hr>
        <a class="sidebarr" href="Admin Dashboard.aspx"><i class="bi bi-house-door-fill"></i>Dashboard</a>
        <a class="sidebarr" href="Add doctor.aspx"><i class="bi bi-people-fill"></i>Doctors</a>
        <a class="sidebarr" href="Schedule Session.aspx"><i class="bi bi-calendar-fill"></i>Schedule</a>
        <a class="sidebarr" href="Manage Appointments.aspx"><i class="bi bi-file-medical-fill"></i>Appointment</a>
        <a class="sidebarr" href="Patients.aspx"><i class="bi bi-person-wheelchair"></i>Patients</a>
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
        <h4 style="font-weight: bold;">Status</h4>
        <div class="card-container">
            <div class="card">
                <h5>1</h5>
                <p>Doctors</p>
            </div>
            <div class="card">
                <h5>4</h5>
                <p>Patients</p>
            </div>
            <div class="card">
                <h5>4</h5>
                <p>New Booking</p>
            </div>
            <div class="card">
                <h5>2</h5>
                <p>Today Session</p>
            </div>
        </div>

        <div class="table-container">
            <div class="table-box">
                <h5>Appointments</h5>
                <asp:Table ID="AppointmentsTbl" runat="server"></asp:Table>
            </div>
            <div class="table-box">
                <h5>Sessions</h5>
                <asp:Table ID="SessionsTbl" runat="server"></asp:Table>
            </div>
        </div>
    </div>
</body>
</html>
