<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Patient Dashboard.aspx.vb" Inherits="Hospital_Appointment_System.Patient_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients Dashboard</title>
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

        .welcome-box {
            background-color: #6c96b3;
            color: white;
            padding: 50px;
            border-radius: 8px;
        }

        .status-card {
            border: 1px solid #000;
            padding: 20px;
            text-align: center;
            border-radius: 8px;
            background: white;
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
            <div class="container">
                <div class="d-flex justify-content-end align-items-center">
                    <!-- Date Section -->
                    <div class="d-flex align-items-center gap-2">
                        <span class="text-secondary small">Today's Date</span>
                        <strong class="fs-6" id="date">2025-02-06</strong>
                        <i class="bi bi-calendar-fill"></i>
                    </div>
                </div>
                <h4 class="fw-bold mb-3">DASHBOARD</h4>
                <div class="d-flex justify-content-between align-items-center">
                    <div class="welcome-box w-100">
                        <h5 class="fw-bold">WELCOME!</h5>
                        <h3 id="usernamee" class="fw-bold" runat="server"></h3>
                        <p>
                            Thank you for joining with us. We are always trying to get you a complete service.<br>
                            You can view your <strong>daily schedule</strong>. 
                        </p>
                        <asp:Button class="btn btn-dark" ID="appointments" Text="View My Appointments" runat="server" OnClick="appointments_Click" />
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-6">
                        <h5 class="fw-bold">Status</h5>
                        <div class="row g-3">
                            <div class="col-md-6">
                                <div class="status-card">
                                    <h2 id="doctorsTotal" runat="server" class="fw-bold">1</h2>
                                    <p>All Doctors</p>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="status-card">
                                    <h2 id="appointmentTotal" runat="server" class="fw-bold">4</h2>
                                    <p>New Appointment</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h5 class="fw-bold">Your Upcoming Appointment</h5>
                        <div class="table-responsive">
                            <asp:Table class="mt-4 table table-bordered" ID="Table1" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>
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

        updateDate(); // Run when page loads
        setInterval(updateDate, 1000); // Update every second
    </script>
</body>

</html>
