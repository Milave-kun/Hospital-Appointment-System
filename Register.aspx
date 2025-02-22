<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Register.aspx.vb" Inherits="Hospital_Appointment_System.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Register</title>
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
    <style>
        body {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f8f9fa;
        }

        .login-container {
            max-width: 900px;
            background: white;
            display: flex;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .login-form {
            padding: 40px;
            flex: 1;
            background: #6c93ae;
            color: white;
        }

            .login-form h2 {
                font-weight: bold;
            }

            .login-form input {
                margin-bottom: 15px;
            }

            .login-form a {
                color: white;
            }

        .btn {
            background-color: #001F3F;
            color: white;
        }

            .btn:hover {
                background-color: #001F3F;
                color: white;
            }

        .login-image {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
            background: white;
            padding: 40px;
            flex-direction: column;
        }

            .login-image img {
                max-width: 80%;
            }
    </style>
</head>
<body>
    <div class="login-container">
        <div class="login-form">
            <h2>USER REGISTER</h2>
            <p>Welcome to eDOC</p>
            <form id="form1" runat="server">
                <asp:TextBox ID="FullnameTxt" runat="server" class="form-control" RequiredFieldValidator1="true" placeholder="Full Name"></asp:TextBox>
                <asp:TextBox ID="usernameTxt" runat="server" class="form-control" RequiredFieldValidator1="true" placeholder="Username"></asp:TextBox>
                <asp:TextBox ID="passwordTxt" runat="server" class="form-control" RequiredFieldValidator1="true" placeholder="Password"></asp:TextBox>
                <asp:TextBox ID="confirmPassTxt" runat="server" class="form-control" RequiredFieldValidator1="true" type="password" placeholder="Confirm Password"></asp:TextBox>

                <button class="btn w-100 mt-2">REGISTER</button>
                <p class="mt-3">Have an account? <a style="color: #001F3F;" href="/Login.aspx">Sign In</a></p>
            </form>
        </div>
        <div class="login-image">
            <img src="/hospital-appointment-system (1).png" alt="Calendar Icon">
            <h4 style="text-align: center; color: #001F3F; font-weight: bold;">Hospital Appointment
                <br />
                System</h4>
        </div>
    </div>
</body>
<script
    src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"
    integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"
    crossorigin="anonymous"></script>
<script
    src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js"
    integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy"
    crossorigin="anonymous"></script>
</html>
