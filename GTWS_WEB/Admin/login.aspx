<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/js/jquery.form.js"></script>
    <meta http-equiv="refresh" content="3600" />
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">
        .login {
            width: 100%;
            height: 250px;
            filter: alpha(opacity=80);
            opacity: 0.8;
            position: absolute;
            background-color: #008DB4;
            top: 30%;
        }

            .login .login-inner {
                position: relative; /*left:-650px;*/
            }

            .login .l {
                float: left;
                height: 250px;
                line-height: 250px;
                margin-left: 10px;
            }

            .login .r {
                float: right;
                margin: 25px 150px 0px 0px;
                height: 250px;
                width: 360px;
            }

                .login .r span {
                    font-weight: bold;
                    font-size: 18px;
                    padding: 7px 7px;
                }

                .login .r .input {
                    font-size: 14px;
                    height: 15px;
                    outline: medium none;
                    padding: 7px 7px;
                    width: 250px;
                    background-color: #3E8CB3;
                    border-right: medium none;
                    border-style: solid none none solid;
                    border-width: 2px medium medium 1px;
                }

        #background {
            left: 0;
            position: absolute;
            bottom: 0;
            width: 100%;
            z-index: -1;
            height: 100%;
        }

        .bg-outer {
            left: 0;
            position: absolute; /*bottom: 0;*/
            width: 100%;
        }

        .bg-inner {
            background-position: 50% 0;
            background-repeat: no-repeat;
            height: 890px;
            min-width: 1000px;
            width: 100%;
        }

        .jszc {
            /*position:relative;*/
            text-align: center;
            font-family: 'Simplified Arabic';
            font-size: 25px;
            margin-top: 45%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).keyup(function (event) {
                if (event.keyCode == 13) {
                    liogin();
                }
            });

        });
        function liogin() {
            var cValue = $("#form1").formSerialize();;
            var cActionMethod = "USER_LOGIN";
            $.ajax({
                type: "POST",
                url: "/api/Handler.ashx",
                cache: false,
                data: cValue + "&action_method=" + cActionMethod,
                success: function (ret) {
                    if (ret > 0) {
                        location.href = "index.aspx";
                    }
                }
            });
        }
        function doGetClick() {
            var obj = new Object();
            obj.id = 1;
            obj.url = "http://www.baidu.com/";
            var vMSG = eval(obj);

            alert(vMSG);
        }
        function frmReset() {
            $("#user_id").val('');
            $("#user_pass").val('');
        }

    </script>
</head>
<body scroll="no" style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; border-style: none; overflow: hidden;">
    <form name="form1" id="form1" runat="server" method="post">
        <div class="l">
            <%-- <h1 style="text-align: left; margin-left: 240px; position: relative; top: 80px; font-size: 35pt">
            商丘市国土资源执法监察监管外网平台--%>
            <h1>
                <img src="../static/images/top/<%= getClientID() %>.png" style="position: absolute; width: 880px; /*font-size: 35pt; */ top: 60px; left: 20px; z-index: 9999;" />
            </h1>
        </div>
        <div class="login" id="login">
            <div class="login-inner">
                <div class="r">
                    <p>
                        <span style="width: 100px; font-size: 21px; font-family: '宋体';">账号：</span>
                        <asp:TextBox ID="user_id" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <span style="width: 100px; font-size: 21px; font-family: '宋体';">密码：</span>
                        <asp:TextBox ID="user_pass" runat="server" TextMode="Password"></asp:TextBox>
                    </p>
                    <p style="text-align: right;">
                        <img src="../static/images/loginbt.png" width="100" height="32" onclick="liogin()"
                            style="cursor: pointer" alt="登录" />&nbsp;&nbsp;
                    <img src="../static/images/logincz.png" width="100" height="32" style="cursor: pointer"
                        alt="重置" onclick="frmReset();" />
                    </p>
                </div>
            </div>
        </div>
        <div class="jszc">
            <span>技术支持：深圳市天龙科技有限公司</span><br />
            <span>联系电话：0755-82957885 0371-66378696</span>
        </div>
    </form>
    <div id="background">
        <div class="bg-outer">
            <div class="bg-inner" style="background-image: url(../static/images/loginbg.jpg);">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        if (window.top.location.href != self.location.href) {
            window.top.location.href = "logoff.aspx";
        }
    </script>
</body>
</html>
