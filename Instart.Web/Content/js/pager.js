function getPageBar(curPage, totalPage, $container) {
    $container.find('*').remove();

    if (totalPage <= 1) {
        return;
    }

    if (curPage > totalPage) {
        curPage = totalPage;
    }

    if (curPage < 1) {
        curPage = 1;
    }

    var pageStr = '<ul>';
    if (curPage == 1) {
        pageStr += "<li class='pre'><a>上一页</a></li>";
    } else {
        pageStr += "<li class='pre'><a href='javascript:void(0)' rel='" + (curPage - 1) + "'>上一页</a></span>";
    }

    for (var i = 1; i <= totalPage; i++) {
        if (i == curPage) {
            pageStr += '<li class="active"><a href="javascript:;" rel="' + i + '">' + i + '</a></li>'
        }
        else {
            pageStr += '<li><a href="javascript:;" rel="' + i + '">' + i + '</a></li>'
        }
    }
    if (curPage >= totalPage) {
        pageStr += "<li class='next'><a>下一页</a></li>";
    } else {
        pageStr += "<li class='next'><a href='javascript:void(0)' rel='" + (parseInt(curPage) + 1) + "'>下一页</a></li>";
    }
    pageStr += '</ul>';

    $container.append(pageStr);
}