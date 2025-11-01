<?php
$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData) {
	print "Произошла ошибка соединения: ";
	print pg_last_error(); exit;
}

$query = 'SELECT * FROM object'; $query1 = 'SELECT * FROM client';
$query2 = 'SELECT * FROM worker'; $query3 = 'SELECT * FROM human';

$result = pg_query($basisData, $query); $result1 = pg_query($basisData, $query1);
$result2 = pg_query($basisData, $query2); $result3 = pg_query($basisData, $query3);
if (!$result) {
	print "Ошибка запроса: ";
	print pg_last_error();
	exit;
}

$object = [[]]; $client = [[]];
$worker = [[]]; $human = [[]];

$i = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	$j = 0;
	foreach ($line as $col_value)
	{
		$object[$i][$j] = $col_value; $j++;
	}
	$i++;
}
$i = 0;
while ($line = pg_fetch_array($result1, null, PGSQL_ASSOC))
{
	$j = 0;
	foreach ($line as $col_value)
	{
		$client[$i][$j] = $col_value; $j++;
	}
	$i++;
}
$i = 0;
while ($line = pg_fetch_array($result2, null, PGSQL_ASSOC))
{
	$j = 0;
	foreach ($line as $col_value)
	{
		$worker[$i][$j] = $col_value; $j++;
	}
	$i++;
}
$i = 0;
while ($line = pg_fetch_array($result3, null, PGSQL_ASSOC))
{
	$j = 0;
	foreach ($line as $col_value)
	{
		$human[$i][$j] = $col_value; $j++;
	}
	$i++;
}

header('Content-type: application/json');
$data['object'] = $object; $data['client'] = $client;
$data['worker'] = $worker; $data['human'] = $human;
echo json_encode($data);

pg_close($con);
//print "<form action='indexClient.html'><p><button type='submit'>вернутся</button></p></form>";
?>
