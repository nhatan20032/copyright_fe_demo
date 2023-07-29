function getListSong() {
    listsong.show();
}

function getReport() {
    report.show();
}

function getUploadFile() {
    uploadfile.show();
}
function getInfo() {
    info.show();
}

function getContract() {
    contract.show();
}
function getAllSong() {
    allsong.show();
}

function btnclickupload() {
    uploadfile.upload();
}
$("#btn_upload_file").click(function (username) {
    var username = $("#username").val();
    $.ajax({
        url: '/account/upload-file',
        data: {
            username: username
        },
        success: function (data) {
            $("#tabs-4").html(data);
        }
    });
});


var info = {
    show: function (username) {
        var username = $("#username").val();
        var username = $("#username").val();
        $.ajax({
            url: "/account/info",
            data: {
                username: username
            },
            success: function (data) {
                //console.log("success");
                $("#tabs-1").html(data);
            },
            error: function () {
                console.log("error");
            }
        });
    }
}
var listsong = {
    show: function (username) {
        var username = $("#username").val();
        $.ajax({
            url: '/account/list-song',
            data: {
                username: username
            },
            success: function (data) {
                $("#tabs-2").html(data);
            },
            error: function () {
                console.log("error when get list song");
            }
        });
    }
}
var uploadfile = {
    show: function (username) {
        var username = $("#username").val();
        $.ajax({
            url: '/account/upload-file',
            data: {
                username: username
            },
            success: function (data) {
                $("#tabs-4").html(data);
            }
        });
    },
    upload: function (upload_file, user_id, film_id) {
        upload_file = $("#div_upload_file").text();
        user_id = $("#user_id").val();
        $.ajax({
            url: '/film_video_image/Update',
            data: {
                upload_file: upload_file,
                user_id: user_id
            },
            type: 'post',
            success: function (data) {
                $("#btn_list_song").click();
            },
            error: function () {
                console.log("fail to upload");
            }
        });
    }
}
var report = {
    show: function (username) {
        var username = $("#username").val();
        $.ajax({
            url: '/account/report',
            data: {
                username: username
            },
            success: function (data) {
                $("#tabs-3").html(data);
            }
        });
    },
}
var contract = {
    show: function (username) {
        var username = $("#username").val();
        $.ajax({
            url: "/account/contract",
            data: {
                username: username
            },
            success: function (data) {
                $("#tabs-5").html(data);
            },
            error: function () {
                console.log("error");
            }
        });
    }
}

var allsong = {
    show: function () {
        $.ajax({
            url: "/account/allsong",
            method: "GET",
            success: function (data) {
                $("#tabs-6").html(data);
            },
            error: function () {
                console.log("error");
            }
        });
    }
}
$(document).ready(getInfo());
$(document).ready(function () {
    $('#register').addClass('current_page_item');
    // Ẩn các lớp có tên là "artist" nếu user.type là "guest"
    if ($("#type").val() === "guest") {
        $('.artist').hide();
        $('.guest').show();
    } else {
        $('.artist').show();
        $('.guest').hide();
    }
});
function showTab(tabName) {
    // Ẩn tất cả các tab
    var tabs = document.querySelectorAll(".entry-content > div");
    for (var i = 0; i < tabs.length; i++) {
        tabs[i].style.display = "none";
    }

    // Hiển thị tab được chọn
    var selectedTab = document.getElementById(tabName);
    selectedTab.style.display = "block";
}

// Thêm sự kiện "click" cho các nút
document.querySelector("#tabs .account-menu li:nth-child(1) a").addEventListener("click", function () {
    showTab("tabs-1");
});
document.querySelector("#tabs .account-menu li:nth-child(2) a").addEventListener("click", function () {
    showTab("tabs-2");
});
document.querySelector("#tabs .account-menu li:nth-child(3) a").addEventListener("click", function () {
    showTab("tabs-3");
});
document.querySelector("#tabs .account-menu li:nth-child(4) a").addEventListener("click", function () {
    showTab("tabs-6");
});