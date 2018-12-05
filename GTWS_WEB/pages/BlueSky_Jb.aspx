<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlueSky_Jb.aspx.cs" Inherits="bluesky_jb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>蓝天卫士举报</title>
    <link href="../static/css/swiper.min.css" rel="stylesheet" type="text/css" />
    <script src="../static/js/swiper.min.js" type="text/javascript"></script>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script src="../static/js/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
           var mySwiper = new Swiper('.swiper-container', {
            navigation: {
              nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
          },
            });
        });
        function LoadInitValue() {
            var vKeyID = getQueryString("ID");
            $("#ID").val(vKeyID);
            $.ajax({
                url: "/api/rest.ashx?action_type=ImgList&action_method=find&ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#adress").val(vInfo.camera_addr);
                    $("#photoinfo").attr("src", vInfo.url);
                    $("#org_id").val(vInfo.org_id);
                    $("#url").val(vInfo.url);
                    var newdate = vInfo.createtime;
                    $("#timeinfo").val(newdate.substr(0, 4) + '年' + newdate.substr(4, 2) + '月' + newdate.substr(6, 2) + '日' + newdate.substr(8, 2) + '分' + newdate.substr(10, 2) + '秒');
                }
            });
        }
        function showinfo() {
            var photoinfo = document.getElementById("photoinfo");
            $("#div2").show();
            $("#addjb").hide();
            photoinfo.style.width = "640px";
            photoinfo.style.height = "480px";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="swiper-container" id="div1" style="margin: 25px 0;">
        <table class="table" style="width: 100%">
            <tr>
                <td style="width: 100px; text-align: right">
                    拍摄照片
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div class="swiper-wrapper" style="text-align: center">
            <div class="swiper-slide">
                <img alt="违法图片" id="photoinfo" name="photo" width="800px" height="680spx"></div>
            <div class="swiper-slide">
                Slide 2</div>
            <div class="swiper-slide">
                Slide 3</div>
        </div>
        <!-- 如果需要导航按钮 -->
        <div class="swiper-button-prev">
        </div>
        <div class="swiper-button-next">
        </div>
    </div>
    <div id="div2" style="display: none">
        <table id="DBGrid" class="table" style="width: 100%">
            <tr>
                <td style="width: 100px; text-align: right">
                    详细地址
                </td>
                <td>
                    <input type="text" id="adress" name="adress" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td style="width: 160px; text-align: right">
                    拍摄时间
                </td>
                <td>
                    <span>
                        <input type="text" id="timeinfo" name="time" readonly="readonly" /></span>
                </td>
            </tr>
            <tr>
                <td style="width: 160px; text-align: right">
                    举报内容
                </td>
                <td>
                    <input type="text" value="照片包含违法内容" id="neirong" name="neirong" contenteditable="true"
                        style="width: 500px" />
                    <font size="3" color="red">*点击文字修改</font>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <br />
                    <input type="button" value="提交" onclick="Event_AjaxSubmit();" style="width: 70px" />
                    <input type="button" value="关闭" onclick="AjaxClose();" style="width: 80px" />
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: center">
        <input type="button" id="addjb" value="添加举报" onclick="showinfo();" style="width: 100px" />
    </div>
    <input type="hidden" id="ID" name="ID" />
    <input type="hidden" id="ORG_ID" name="ORG_ID" />
    <input type="hidden" id="URL" name="URL" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="action_type" Value="WXJB" runat="server" />
    <asp:HiddenField ID="action_method" Value="savegtws" runat="server" />
    </form>
</body>
</html>
