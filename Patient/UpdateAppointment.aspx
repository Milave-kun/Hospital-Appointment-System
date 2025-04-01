<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UpdateAppointment.aspx.vb" Inherits="Hospital_Appointment_System.UpdateAppointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients Dashboard - Scheduled Sessions</title>
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
            <h4 id="selectedUsername" class="fw-bold" runat="server"></h4>
            <p>
                <asp:Literal ID="selectedRole" runat="server"></asp:Literal>
            </p>

            <asp:Button class="btn w-100" ID="logoutBtn" Text="LOG OUT" runat="server" OnClick="logoutBtn_Click" />
            <hr>
            <a class="sidebarr" href="Patient Dashboard.aspx"><i class="bi bi-house-door-fill"></i>Dashboard</a>
            <a class="sidebarr" href="All Doctors.aspx"><i class="bi bi-briefcase-fill"></i>All Doctors</a>
            <!-- <a class="sidebarr" href="Sessions Patient.aspx"><i class="bi bi-clock-fill"></i>Scheduled Sessions</a> -->
            <a class="sidebarr" href="Appointments Patient.aspx"><i class="bi bi-bookmark-fill"></i>My Appointments</a>
        </div>

        <div class="main-content">
            <div class="d-flex justify-content-end align-items-center mb-3">
                <!-- Date Section -->
                <div class="d-flex align-items-center gap-2">
                    <span class="text-secondary small">Today's Date</span>
                    <strong class="fs-6" id="date">2025-02-06</strong>
                    <i class="bi bi-calendar-fill"></i>
                </div>
            </div>

            <h4 style="font-weight: bold;" class="mb-5">UPDATE SELECTED APPOINTMENT</h4>
            <div class="modal-body d-flex flex-column gap-3">
                <asp:TextBox ID="txtAppointmentID" runat="server" class="form-control" TextMode="Number" ReadOnly="true" />
                <asp:TextBox ID="txtAppointmentTitle" runat="server" class="form-control" />
                <asp:TextBox ID="txtProfession" runat="server" class="form-control" />
                <asp:TextBox ID="txtDoctors" runat="server" class="form-control" />
                <asp:TextBox ID="txtDate" runat="server" class="form-control" TextMode="Date" max="2028-12-31" />
                <asp:TextBox ID="txtTime" runat="server" class="form-control" TextMode="Time" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn" Text="Update Appointment" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </form>
    <script>
        function updateDate() {
            let now = new Date();
            let formattedDate = now.getFullYear() + '-' +
                String(now.getMonth() + 1).padStart(2, '0') + '-' +
                String(now.getDate()).padStart(2, '0');
            document.getElementById("date").innerText = formattedDate;
        }
    </script>
</body>
</html>
