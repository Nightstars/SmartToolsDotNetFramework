@*
    Files generated by SmartTools.Net Codeless
    $GenDate$
*@

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>$title$-修改</title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <link href="~/app/lib/layui-v2.5.4/css/layui.css" rel="stylesheet" />
    <link href="~/app/css/combo.css" rel="stylesheet" />
    <link href="~/app/css/combobox.css" rel="stylesheet" />
    <link href="~/app/css/textbox.css" rel="stylesheet" />
    <link href="~/app/css/common.single-page.opdialog-1.0.css" rel="stylesheet" />
    <link href="~/app/css/common-3.0.css" rel="stylesheet" />
    <link href="~/app/css/date.css" rel="stylesheet" />
</head>
<body>
    <form id="EditForm" class="layui-form drawer-form" onkeydown="if(event.keyCode==13){return false}">
        @Html.AntiForgeryToken()
        <input type="hidden" name="SeqNo" value="@Model.SeqNo" />
        <input type="hidden" name="Site" value="@Model.Site" />
        <div>

            $formArea$

        </div>

        <div style="margin-top:15px;text-align:right;">
            <button class="layui-btn layui-btn-primary layui-bg-blue" type="submit">确定</button>
        </div>

    </form>

    <script src="~/app/js/jquery.min.js"></script>
    <script src="~/app/lib/layui-v2.5.4/layui.js"></script>
    <script src="~/app/js/jquery.validate.min.js"></script>
    <script src="~/app/js/common-3.0.js"></script>
    <script src="~/app/js/easyloader.js"></script>
    <script src="~/app/js/jquery.parser.js"></script>
    <script src="~/app/js/jquery.linkbutton.js"></script>
    <script src="~/app/js/jquery.panel.js"></script>
    <script src="~/app/js/jquery.validatebox.js"></script>
    <script src="~/app/js/jquery.tooltip.js"></script>
    <script src="~/app/js/jquery.textbox.js"></script>
    <script src="~/app/js/jquery.combo.js"></script>
    <script src="~/app/js/jquery.combobox.js"></script>
    <script src="~/app/js/language.js"></script>

    <script type="text/javascript">
        var initCopGNoFlag = false;
        layui.use(['form', 'laydate', 'element', 'layer', 'laydate'], function () {
           var form = layui.form,
                layer = layui.layer;
           var laydate = layui.laydate;

            var loadingIndex = layer.load(1, {
                shade: [0.4, '#fff'] //0.1透明度的白色背景
            });

            /********************此处为日期渲染示例，请根据实际业务调整 begin********************/
            /********************SmartTools.Net Codeless********************/

            /*
            laydate.render({
                    elem: '#xxx',
                    type: 'date',
                    value:'@DateTime.Now.ToString("yyyy-MM-dd")',
                 trigger: 'click',
            })
            */

            /********************此处为日期渲染示例，请根据实际业务调整 end********************/

            active = {

                //提交
                submit: function (form) {
                    var _index = showLoading()
                    var data = $(form).serializeJson();
                    $.ajax({
                        type: "POST",
                        url: "Edit",
                        data: data,
                        dataType: "json",
                        success: function (result) {
                            hideLoading(_index)
                            if (result.Success) {
                                layer.alert('修改成功！', { icon: 1, closeBtn: 0, title: '系统提示' }, function (index) {
                                    layer.close(index);
									parent.active.search()
                                    active.cancel();
                                });
                            } else {
                                layer.alert('修改失败！' + result.Message, { icon: 2, closeBtn: 0, title: '系统提示' }, function (index) {
                                    layer.close(index);
                                });
                            }
                        },
                        error: function (e) {
                            hideLoading(_index)
                            layer.alert('修改失败:' + e.status, { icon: 2, closeBtn: 0, title: '系统提示' }, function (index) {
                                layer.close(index);
                            });
                        }
                    })
                },

                //取消
                cancel: function () {
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                },

                //初始化
                init: function () {
                    $(".lg-combobox").lgcombobox();

                    $('.layui-btn').on('click', function () {
                        var type = $(this).data('type');
                        active[type] ? active[type].call(this) : '';
                    })

                    /********************此处为下拉框初始化示例，请根据实际业务调整 begin********************/
                    /********************SmartTools.Net Codeless********************/

                    //普通下拉框
                    /*
                    $('#xxx').lgcombobox({
                        onSelect: function (n, o) {
                            $('#xxxName').val(n.Name)
                        }
                    })
                    */

                    //联动下拉
                    /*
                    $('#xxx').lgcombobox({
                        onSelect: function (n, o) {
                            $.ajax({
                                type: "POST",
                                url: "xxx",
                                data: { copGNo: n.Code },
                                success: function (result) {
                                    if (result.Success) {

                                        $('#xxx').val(result.Data.xxx);
                                        $('#xxx').combobox('setValue', result.Data.xxx);

                                        hideLoading(loadingIndex);
                                    } else {
                                        hideLoading(loadingIndex);
                                    }
                                },
                                error: function (e) {
                                    hideLoading(loadingIndex);
                                }
                            })
                        }
                    })
                    */

                    /********************此处为下拉框初始化示例，请根据实际业务调整 end********************/

                },
            }

            active.init()
            layer.close(loadingIndex);
            $('body').css('visibility', 'visible')
            ValidateForm('EditForm', active.submit);
        });

    </script>
</body>
</html>