
// Lấy các phần tử cần thao tác
var gioiThieu = document.getElementById("gioi-thieu");
var baiHat = document.getElementById("bai-hat");
var tabGioiThieu = document.querySelector(".tabs--links a[href='#gioi-thieu']");
var tabBaiHat = document.querySelector(".tabs--links a[href='#bai-hat']");

// Thêm sự kiện click vào tab "Bài hát"
tabBaiHat.addEventListener("click", function () {
    gioiThieu.style.display = "none"; // Ẩn phần tử có id="gioi-thieu"
    baiHat.style.display = "block"; // Hiển thị phần tử có id="bai-hat"
    tabBaiHat.classList.add("active"); // Thêm lớp active vào tab "Bài hát"
    tabGioiThieu.classList.remove("active"); // Xóa lớp active khỏi tab "Giới thiệu"
});

// Thêm sự kiện click vào tab "Giới thiệu"
tabGioiThieu.addEventListener("click", function () {
    gioiThieu.style.display = "block"; // Hiển thị phần tử có id="gioi-thieu"
    baiHat.style.display = "none"; // Ẩn phần tử có id="bai-hat"
    tabGioiThieu.classList.add("active"); // Thêm lớp active vào tab "Giới thiệu"
    tabBaiHat.classList.remove("active"); // Xóa lớp active khỏi tab "Bài hát"
});