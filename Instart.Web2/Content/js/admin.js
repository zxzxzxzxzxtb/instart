//上传图片预览
function imagePreview(file, $preObj) {
    var r = new FileReader();
    r.readAsDataURL(file);
    $(r).load(function () {
        $preObj.attr("src", this.result);
    });
}