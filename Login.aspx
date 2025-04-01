<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Hospital_Appointment_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>
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

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <style>
        body {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f8f9fa;
            font-family: "Poppins", sans-serif;
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

            .password-wrapper {
    position: relative;
    display: flex;
    align-items: center;
}

.toggle-password {
    position: absolute;
    right: 10px;
    top: 50%;
    transform: translateY(-50%);
    cursor: pointer;
    font-size: 18px;
    color: #555;
}

.form-control {
    padding-right: 35px; /* Ensure space for the icon */
    height: 40px; /* Adjust height if needed */
}

    </style>
</head>
<body>
    <div class="login-container">
    <div class="login-form">
        <h2>USER LOGIN</h2>
        <p>Welcome to eDOC</p>
        <form id="form1" runat="server">
            <asp:TextBox ID="usernameTxt" runat="server" class="form-control" RequiredFieldValidator1="true" placeholder="Username"></asp:TextBox>

            <!-- Password field with eye icon -->
            <div class="password-wrapper">
                <asp:TextBox ID="passwordTxt" runat="server" class="form-control" RequiredFieldValidator1="true" type="password" placeholder="Password"></asp:TextBox>
                <span class="toggle-password" onclick="togglePassword()">
                    <i class="fa fa-eye" id="eyeIcon"></i>
                </span>
            </div>

            <div class="d-flex justify-content-between">
                <a href="#">Forgot Password?</a>
            </div>

            <asp:Button class="btn w-100 mt-3" ID="btnLogin" Text="LOGIN" runat="server" OnClick="btnLogin_Click" />
            <p class="mt-3">Don't have an account? <a style="color: #001F3F;" href="/Register.aspx">Sign Up</a></p>
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
<script>
    function togglePassword() {
        var passwordField = document.getElementById('<%= passwordTxt.ClientID %>');
        var eyeIcon = document.getElementById("eyeIcon");

        if (passwordField.type === "password") {
            passwordField.type = "text";
            eyeIcon.classList.remove("fa-eye");
            eyeIcon.classList.add("fa-eye-slash");
        } else {
            passwordField.type = "password";
            eyeIcon.classList.remove("fa-eye-slash");
            eyeIcon.classList.add("fa-eye");
        }
    }
</script>
<script
    src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"
    integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"
    crossorigin="anonymous"></script>
<script
    src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js"
    integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy"
    crossorigin="anonymous"></script>
</html>
