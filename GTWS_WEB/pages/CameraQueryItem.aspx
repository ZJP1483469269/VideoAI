<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CameraQueryItem.aspx.cs"
    Inherits="CameraQueryItem" %>

<div id="DBGrid">
    <table style="width: 100%" class="table table-bordered">
        <tr>
            <td style="width: 100px">所属县区
            </td>
            <td>
                <select id="COUNTY" name="COUNTY" onchange="LoadVillage();">
                </select>
            </td>
        </tr>
        <tr>
            <td>所属乡镇
            </td>
            <td>
                <select id="VILLAGE" name="VILLAGE">
                </select>
            </td>
        </tr>
        <tr>
            <td>详细位置
            </td>
            <td>
                <input type="text" id="ADDR" name="ADDR" style="width: 300px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <input type="button" value="查询" onclick="doQuery();" />
            </td>
        </tr>
    </table>
    <table style="width: 100%" class="table table-bordered">
        <tr>
            <td>所属县区
            </td>
            <td>所属乡镇
            </td>
            <td>详细位置
            </td>
            <td>定位
            </td>
        </tr>
        <tbody id="TABLE_TBODY">
        </tbody>
    </table>
</div>
