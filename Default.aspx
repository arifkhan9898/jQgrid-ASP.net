<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JQGrid Demo - Ocean Cloudy</title>
  
	<link type="text/css" href="assets/jquery/css/ocean-cloudy-min/jquery-ui.min.css" rel="stylesheet" />
	<link type="text/css" href="assets/jqGrid/css/ui.jqgrid.css" rel="stylesheet" />
	
	<script type="text/javascript" src="assets/jqGrid/js/jquery-1.9.0.min.js"></script>
	<script type="text/javascript" src="assets/jquery/js/jquery-ui-1.10.3.custom.js"></script>
	<script type="text/javascript" src="assets/jqGrid/js/jquery.jqGrid.src.js"></script>
	<script type="text/javascript" src="assets/jqGrid/js/i18n/grid.locale-en.js"></script>
	
	<script type="text/javascript">
	$.jgrid.no_legacy_api = true;
	$.jgrid.useJSON = true;
		
	$(document).ready(function () {
	
		jQuery("#tOceanC_ds_list").jqGrid({
			url:"Default.aspx?cmd=select",
			datatype: "json",
			colNames:['id','Username','Photo','Comment'],
			colModel:[
				{name:"id",hidden:true, index:"id",jsonmap:"id"},
				{name:"username",editable:true, index:"username", width:250, align:"center", jsonmap:"username"},
				{name:"photo",editable:true, index:"photo", width:250, align:"center", jsonmap:"photo",formatter:customPhoto},
				{name:"comment",editable:true, index:"comment", width:250, align:"center", jsonmap:"comment"}
			],
			rowNum:10,
			height: "100%",
			rownumbers: true,
			autowidth: true,
			shrinkToFit: false,
			rowList:[10,20,30,50,100],
			pager: jQuery("#OceanC_pager"),
			viewrecords: true,
			multiselect: true, 
			sortorder: "desc",
			caption:"List of Users",
			editurl:"Default.aspx?cmd=delete",
			jsonReader: { repeatitems : false, id: "0" }
		});

		jQuery("#tOceanC_ds_list").jqGrid("navGrid", "#OceanC_pager", {add:false, edit:false, del:true, search:false}, {}, {}, {}, {multipleSearch:false});

		
	});
	function customPhoto(cellVal, options, rowObject){
		return "<img src='assets/img/" + cellVal + "' height='50' width='50' >";
	}


	</script>

	<style>
		.formats{
			border-bottom: 1px dashed skyblue;
			color: #AAAAAA;
			font-family: arial;
			padding: 10px;
			text-align: right;
		}
		.formats-2{
			 border-top: 1px solid skyblue;
			bottom: 0;
			color: #999999;
			font-family: arial;
			padding: 5px;
			position: absolute;
			text-align: left;
			font-size: 12px;
		}
	</style>
	
</head>
<body>
    <form id="form1" runat="server">
		<h2 class="formats">JQGRID with ASP.Net - Ocean Cloudy</h2>
		<div>
			
			<div>
				<!--	jQgrid container 	-->
				<table id="tOceanC_ds_list"></table>
			</div>
			<!--	Paging footer-->
			<div id="OceanC_pager"></div>		
		
		</div>
	<p class="formats-2">Code by Atul Savaliya - visit at <a href="http://www.oceancloudy.com">www.oceancloudy.com</a></p>
    </form>
</body>
</html>
