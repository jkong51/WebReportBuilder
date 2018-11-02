<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="FYP.WebForm4" %>

<!DOCTYPE html>
<html>
<head>
<script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
  <meta charset="utf-8">
  <title>JS Bin</title>
</head>
<body>
<table id="mainTable">
	<tr>
		<td>Row 1</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 2</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 3</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 4</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 5</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 6</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 7</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 8</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 9</td>
		<td>Dummy Data</td>
	</tr>
	<tr>
		<td>Row 10</td>
		<td>Dummy Data</td>
	</tr>
</table>

<script type="text/javascript">
jQuery(document).ready(function() {

	var $mainTable = $("#mainTable");
	var splitBy = 5;
	//$mainTable.find ( "tr" ).slice( splitBy ).css( "background-color", "red" );
	var rows = $mainTable.find ( "tr" ).slice( splitBy );
	var $secondTable = $("#mainTable").parent().append("<table id='secondTable' style='border:solid 1px'><tbody></tbody></table>");
	$secondTable.find("tbody").append(rows);
	$mainTable.find ( "tr" ).slice( splitBy ).remove();

});
</script>



</body>
</html>