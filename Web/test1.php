<?php

$basisData = pg_connect('host=localhost dbname=new user=postgres password=123');
if (!$basisData) {
	print "<p>Произошла ошибка соединения: ";
	print pg_last_error();
	exit;
}
else
	print "<p>Подключено";

$query = 'SELECT DISTINCT f.nazvanie "f_nazvanie", p2.fio "p_fio" '.
	'FROM prepodav p2 '.
	'INNER JOIN ocenki o ON p2.id=o.id_p '.
	'INNER JOIN student s ON s.id=o.id_s '.
	'INNER JOIN facult f ON f.id=s.id_f '.
	"WHERE f.nazvanie = 'Географии' ".
	'ORDER BY f.nazvanie, p2.fio';
$result = pg_query($basisData, $query);
if (!$result) {
	print "<p>Ошибка запроса: <p>";
	print pg_last_error();
	exit;
}

print "<table border = 1><tr>";
$p = pg_num_fields($result);
for ($i = 0; $i < $p; $i++)
	print "<th>".pg_field_name($result, $i)."</th>";
print "</tr>";

while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	print "<tr>";
	
	foreach ($line as $col_value)
		print "<td>".$col_value."</td>";

	print "</tr>";
}
print "</table><br>";

$query = 'SELECT DISTINCT p2.fio "p_fio" '.
	'FROM prepodav p2 '.
	'INNER JOIN ocenki o ON p2.id=o.id_p '.
	'INNER JOIN student s ON s.id=o.id_s '.
	'INNER JOIN facult f ON f.id=s.id_f '.
	"EXCEPT ".
	'SELECT p2.fio '.
	'FROM prepodav p2 '.
	'INNER JOIN ocenki o ON p2.id=o.id_p '.
	'INNER JOIN student s ON s.id=o.id_s '.
	'INNER JOIN facult f ON f.id=s.id_f '.
	"WHERE f.nazvanie = 'Географии'";
$result = pg_query($basisData, $query);
if (!$result) {
	print "<p>Ошибка запроса: <p>";
	print pg_last_error();
	exit;
}

print "<table border = 1><tr>";
$p = pg_num_fields($result);
for ($i = 0; $i < $p; $i++)
	print "<th>".pg_field_name($result, $i)."</th>";
print "</tr>";

while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	print "<tr>";
	
	foreach ($line as $col_value)
		print "<td>".$col_value."</td>";

	print "</tr>";
}
print "</table>";

pg_close($con);
?>
