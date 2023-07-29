// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ValidateEmail(email) {
    let regexEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (email.match(regexEmail)) {
        return true;
    } else {
        return false;
    }
};
var sv = sv || {};
!function (window, $, sv) {
    sv.log = function (msg) {
        if (typeof console == 'object') {
            window.console.log(msg);
        }
    };
    sv.alert = function ($msg, callback) {
        sv.error();
    };
    sv.error = function ($msg, callback) {
        if ($msg == "login") {
            login.show();
            return false;
        } else if ($msg == "redirect") {
            window.location.href = "/"
        }
        Swal.fire({
            title: 'Thông báo...',
            type: 'error', 
            text: $msg,
        }).then(function (result) {
            if (typeof callback == 'function') {
                callback.call();
            }
        })
    };

    sv.confirm = function ($msg, callback) {
        Swal.fire({
            title: 'Xác nhận',
            type: 'info',
            text: $msg,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Đóng",
        }).then((result) => {
            if (result.value) {
                if (typeof callback == 'function') {
                    callback.call();
                }
            }
        })
    };
    sv.success = function ($msg, callback) {
        Swal.fire({
            title: 'Thành công...',
            type: 'success', 
            text: $msg,
        }).then(function (result) {
            if (typeof callback == 'function') {
                callback.call();
            }
        })
    };
    sv.mask = function (show) {
        show = show || false;
        if (show == true) {
            $('#loading').addClass('in');
        } else {
            $('#loading').removeClass('in');
        }
    };

    sv.getFormData = function ($form) {
        var formData = new FormData();
        var unindexed_array = $form.serializeArray();
        $.map(unindexed_array, function (n, i) {
            formData.append(n['name'], n['value']);
        });
        return formData;
    };

    sv.getFormObject = function ($form) {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};
        $.map(unindexed_array, function (n, i) {
            $e = $('[name="' + n['name'] + '"]');
            var na = (n['name']).replace('[]', '');
            // select multiple
            if ($e[0] && $e[0].tagName == "SELECT" && $e.attr('multiple') != undefined) {
                indexed_array[na] = $e.val();
            }
            // input check box
            else if ($e[0] && $e[0].tagName == "INPUT" && $e[0].type == "checkbox") {
                var val = [];
                for (var x = 0; x < $e.length; x++) {
                    var $element = $($e[x]);
                    if ($element.is(":checked"))
                        val.push($element.val());
                }
                indexed_array[na] = val;
            } else {
                indexed_array[na] = n['value'];
            }
        });
        return indexed_array;
    };

    sv.fromAjax = function (o, showMask, callback) {
        var url = o.attr("action");
        var type = o.attr("method");
        var data = sv.getFormObject(o);
        $.ajax({
            url: url,
            type: type,
            data: data,
            cache: true,
            beforeSend: function () {
                if (showMask)
                    sv.mask(true);
            },
            complete: function () {
                if (showMask)
                    sv.mask(false);
            },
            success: function (response) {
                if (callback && typeof callback == 'function') {
                    callback.call(this, response);
                }
            },
            error: function (e) {
                sv.alert("Có lỗi trong quá trình xử lý");
            }
        });

    };

    sv.jsonAjax = function (url, data, showMask, callback) { 
        $.ajax({
            url: url,
            type: "post",
            data: data,
            cache: true,
            beforeSend: function () {
                if (showMask)
                    sv.mask(true);
            },
            complete: function () {
                if (showMask)
                    sv.mask(false);
            },
            success: function (response) {
                if (callback && typeof callback == 'function') {
                    callback.call(this, response);
                }
            },
            error: function (e) {
                sv.alert("Có lỗi trong quá trình xử lý");
            }
        });

    };


}(window, window.jQuery, window.sv);;



