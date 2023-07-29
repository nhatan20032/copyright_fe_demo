$(document).ready(function () {
    function login() {
        if ($('#username').val().length <= 0 || $('#password').val().length <= 0) {
            alert("Yêu cầu nhập thông tin tài khoản.");
            return false
        }
        $.ajax({
            url: "/account/login",
            method: "post",
            data: {
                username: $('#username').val(),
                password: $('#password').val(),
            },
            success: function (b) {
                if (b.int_status == 1) {
                    localStorage.setItem("lala_user_token", b.data.token);
                    localStorage.setItem("userid", b.data.userid);
                    window.location.href = "/account/thong-tin-ca-nhan"
                } else {
                    Swal.fire({
                        title: "Tài khoản hoặc mật khẩu không đúng",
                        type: "error"
                    })
                }
            }
        });
    }
    $("#btnLogin").click(function () {
        login();
    });
    $("#divLogin input").on("keyup", function (a) {
        if (a.keyCode == 13) {
            login();
        }
    });
    function logout() {
        localStorage.setItem("lala_user_token", "");
        localStorage.removeItem("lala_user_token");
        $.ajax({
            url: "/account/logout",
            method: "POST",
            success: function (data) {
                window.location.href = "/"
            },
            error: function (xhr) {
                alert('Lỗi');
            }
        });
    }
    $("#logout_submit").on("click", function () {
        logout();
    });
    $("#register_submit").on("click", function () {
        $("#register_submit").attr("disabled", true);
        register();
        $("#register_submit").attr("disabled", false);
    });
    $("#divRegister input[name=password]").on("keyup", function (a) {
        if (a.keyCode == 13) {
            register();
        }
    });

    function register() {
        let user = $('#username_reg').val();
        let pass = $('#password_reg').val();
        let full = $('#fullname_reg').val();
        let email = $('#email_reg').val();
        let type = $('#type_reg').val();
        let repass = $('#repassword').val();
        if (!user || !pass || !email) {
            alert("Yêu cầu nhập thông tin tài khoản.");
            return;
        }
        if (pass != repass) {
            alert("2 mật khẩu nhập không giống nhau.");
            return;
        }
        if (full) {
            let ds = ["nứng", "chó", "dog", "loz", "mẹ", "bố", "lolz", "lone", "lồz", "lồn", "đĩ", "địt", "dịt", "djt", "đjt", "me", "đỉ", "cặc", "cc", "ncc", "fuck", "bitch", "đụ", "đm", "dm", "đmm", "Đmm", "dmm", "Dmm", "cl", "clm", "clmm", "clgt", "đéo"];
            ds.forEach(function (item) {
                if (full.includes(item)) {
                    $('#fullname_reg').val('');
                    full = 'Banned account';
                }
            });
        }
        $.ajax({
            url: "/account/register",
            method: "post",
            data: {
                fullname: full,
                username: user,
                password: pass,
                email: email,
                type: type,
            },
            success: function (data) {
                if (data) {
                    if (data != "duplicate") {
                        sv.success("Đăng ký thành công", function () {
                            localStorage.setItem("lala_user_token", data.token);
                            localStorage.setItem("userid", data.id);
                            window.location.href = "/account/thong-tin-ca-nhan"
                        })
                    } else {
                        sv.error("Tài khoản hoặc email đã tồn tại", function () { });
                    }
                } else {
                    $("#sv_error_register .sv_error").html('<label class="error" for="username" style="display: block;">Có lỗi trong quá trình đăng ký, vui lòng liên hệ quản trị viên</label>')
                }
            }
        });
    };
});