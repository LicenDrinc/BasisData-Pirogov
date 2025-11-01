<?php

$basisData = pg_connect('host=localhost dbname=new user=postgres password=123');
if (!$basisData) {
	print "<p>Произошла ошибка соединения: ";
	print pg_last_error();
	exit;
}
else
	print "<p>Подключено";

$query = 'SELECT f.nazvanie "f_nazvanie", s.fio "s_fio", p.predmet, o.ocenka, p2.fio "p_fio", o.dt '.
	'FROM facult f '.
	'INNER JOIN student s ON f.id=s.id_f '.
	'INNER JOIN ocenki o ON s.id=o.id_s '.
	'INNER JOIN predmeti p ON p.id=o.id_predm '.
	'INNER JOIN prepodav p2 ON p2.id=o.id_p '.
	//'WHERE 5=all(SELECT ocenka FROM ocenki WHERE id_s=s.id) '.
	//'WHERE 5=o.ocenka '.
	'ORDER BY f.nazvanie, s.fio';
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
