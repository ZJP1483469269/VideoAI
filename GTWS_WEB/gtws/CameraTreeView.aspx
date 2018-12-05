<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CameraTreeView.aspx.cs" Inherits="CameraTreeView" %>

<div class="accordion-heading" style="text-align: center">
    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
        摄像机 </a>
</div>
<div id="collapseOne" class="accordion-body collapse" style="height: 0px;">
    <div class="accordion-inner">
        <div id="TVS" style="width: 100%; height: 600px; overflow-y: scroll; overflow-x: scroll;">
            <div class="zTreeDemoBackground left">
                <ul id="TV_ID" class="ztree">
                </ul>
            </div>
        </div>
    </div>
</div>
