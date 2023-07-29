function convertToSlug(str) {
    var from = "àáãảạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệđùúủũụưừứửữựòóỏõọôồốổỗộơờớởỡợìíỉĩịäëïîöüûñçýỳỹỵỷ",
        to = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeduuuuuuuuuuuoooooooooooooooooiiiiiaeiiouuncyyyyy";
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(RegExp(from[i], "gi"), to[i]);
    }

    str = str.toLowerCase()
        .trim()
        .replace(/[^a-z0-9\-]/g, '')
        .replace(/đ/g, 'd')
        .replace(/-+/g, '').toLowerCase()
        .replace(/ /g, '')
        .replace(/[^\w-]+/g, '');

    return str;
}
$('#username_reg').keyup(function () {
    let content = $(this).val();
    if (content) {
        let ct = convertToSlug(content);
        $("#username_reg").val(ct);
    }
});
$('#fullname_reg').keyup(function () {
    let content = $(this).val();
    if (content) {
        content = content.toLocaleLowerCase();
    }
    let ds = ["nứng", "chó", "dog", "loz", "mẹ", "bố", "lolz", "lone", "lồz", "lồn", "đĩ", "địt", "dịt", "djt", "đjt", "me", "đỉ", "cặc", "cc", "ncc", "fuck", "bitch", "đụ", "đm", "dm", "đmm", "Đmm", "dmm", "Dmm", "cl", "clm", "clmm", "clgt", "đéo"];
    ds.forEach(function (item) {
        if (content.includes(item)) {
            $('#fullname_reg').val('');
            return;
        }
    });
});
//js view
const checkTerms = () => {
    let termsCheck = document.getElementById('agree-terms');
    let btnRe = document.getElementById('register_submit');
    if (termsCheck.checked == true) {
        btnRe.classList.remove("disabled");
        btnRe.disabled = false;
    } else {
        btnRe.classList.add("disabled");
        btnRe.disabled = true;
    }
}
// đổi view
    let signUp = document.getElementById("divRegister");
    let logIn = document.getElementById("divLogin");

    const changeToLogin = () => {
        signUp.classList.add("hidden");
        logIn.classList.remove("hidden");
    }

    const changeToSignup = () => {
        signUp.classList.remove("hidden");
        logIn.classList.add("hidden");
    }