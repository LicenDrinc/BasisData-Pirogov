<?php
function printExit($d = '')
{
	print $d; exit;
}

function readTable()
{
	global $line, $line1, $p;
	if (!$line1)
	{
		$i = 0;
		foreach ($line as $col_value)
		{
			print "<td".($p==1?" class=\"boxTd2\"":($i==0?" class=\"boxTd0\"":($i==$p-1?" class=\"boxTd1\"":""))).">".$col_value."</td>";
			$i++;
		}
	}
	else
	{
		foreach ($line as $col_value)
			print "<td>".$col_value."</td>";
	}
}

function HavingOrAnd()
{
	global $query, $checkAnd;
	if ($checkAnd) $query .= ' AND ';
	else $query .= ' WHERE ';
}

function toQuery($d = '')
{
	global $query, $checkAnd;
	$query .= $d; $checkAnd = true;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$table = $_POST['type_table'];
	
	if ($table == '_order')
	{
		$date = $_POST['date'];
		$name_client = $_POST['name_client'];
		$name_worker = $_POST['name_worder'];
		$phone_number_client = $_POST['phone_client'];
		$phone_number_worker = $_POST['phone_worder'];
		$id_object = $_POST['id_object'];
		$id_order = $_POST['id_order'];
	}
	else if ($table == 'object')
	{
		$id_object = $_POST['id_object'];
	}
	else if ($table == 'client')
	{
		$name_client = $_POST['name_client'];
		$phone_number_client = $_POST['phone_client'];
	}
	else if ($table == 'worker')
	{
		$name_worker = $_POST['name_worder'];
		$phone_number_worker = $_POST['phone_worder'];
	}
	else if ($table == 'human')
	{
		$phone_number = $_POST['phone_number'];
		$name = $_POST['name'];
	}
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT ';

if ($table == "_order")
	$query .= 'h1.name AS "Имя клиента", h1.phone_number AS "Номер телефон клиента", h2.name AS "Имя работника", '.
		'h2.phone_number AS "Номер телефон работника", _o.date AS "Дата", _o.failure AS "Поломка", '.
		'o.name AS "Название объекта", _o.price AS "Цена", _o.clients_words AS "Со слов клиента", '.
		'_o.status AS "Статус", _o.id_order AS "Заказ" FROM _order _o '.
		'INNER JOIN client c ON _o.id_client=c.id INNER JOIN human h1 ON c.id_human=h1.id '.
		'INNER JOIN worker w ON _o.id_worker=w.id INNER JOIN human h2 ON w.id_human=h2.id '.
		'INNER JOIN object o ON _o.id_object=o.id ';
else if ($table == "client")
	$query .= 'h.name AS "Имя клиента", h.phone_number AS "Номер телефон клиента" FROM '.
		$table.' c INNER JOIN human h ON c.id_human=h.id ';
else if ($table == "worker")
	$query .= 'h.name AS "Имя клиента", h.phone_number AS "Номер телефон клиента" FROM '.
		$table.' c INNER JOIN human h ON c.id_human=h.id ';
else if ($table == "object")
	$query .= 'name AS "Название объекта" FROM '.$table.' ';
else if ($table == "human")
	$query .= 'name AS "Имя", phone_number AS "Телефон клиента" FROM '.$table.' ';

$checkAnd = false;
if ($table == '_order')
{
	if ($date != null) toQuery(' WHERE _o.date = \''.$date.'\'');
	if ($name_client != '')
	{ HavingOrAnd(); toQuery('c.id_human = '.$name_client); }
	if ($phone_number_client != '')
	{ HavingOrAnd(); toQuery('c.id_human = '.$phone_number_client); }
	if ($name_worker != '')
	{ HavingOrAnd(); toQuery('w.id_human = '.$name_worker); }
	if ($phone_number_worker != '')
	{ HavingOrAnd(); toQuery('w.id_human = '.$phone_number_worker); }
	if ($id_object != '')
	{ HavingOrAnd(); toQuery('_o.id_object = '.$id_object); }
	if ($id_order != null)
	{ HavingOrAnd(); toQuery('_o.id_order = '.$id_order); }
}
else if ($table == 'client')
{
	if ($name_client != '') toQuery(' WHERE c.id_human = '.$name_client);
	if ($phone_number_client != '')
	{ HavingOrAnd(); toQuery('c.id_human = '.$phone_number_client); }
}
else if ($table == 'worker')
{
	if ($name_worker != '') toQuery(' WHERE c.id_human = '.$name_worker);
	if ($phone_number_worker != '')
	{ HavingOrAnd(); toQuery('c.id_human = '.$phone_number_worker); }
}
else if ($table == 'object')
{
	if ($id_object != '') toQuery(' WHERE id = '.$id_object);
}
else if ($table == 'human')
{
	if ($name != '') toQuery(' WHERE id = '.$name);
	if ($phone_number != '')
	{ HavingOrAnd(); toQuery('id = '.$phone_number); }
}
$query .= ' ';

if ($table == "_order") $query .= ' ORDER BY _o.id ';
else if ($table == "client") $query .= ' ORDER BY c.id ';
else if ($table == "worker") $query .= ' ORDER BY c.id ';
else if ($table == "object") $query .= ' ORDER BY id ';
else if ($table == "human") $query .= ' ORDER BY id ';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

print "<table border=\"0\" class='boxTab0' rules='all'><tr class='boxTab1'>";
$p = pg_num_fields($result);
for ($i = 0; $i < $p; $i++)
	print "<th".($p==1?" class=\"boxTh2\"":($i==0?" class=\"boxTh0\"":($i==$p-1?" class=\"boxTh1\"":""))).">".pg_field_name($result, $i)."</th>";
print "</tr>";

$line1 = pg_fetch_array($result, null, PGSQL_ASSOC); $line = 0;
while ($line = $line1)
{
	$line1 = pg_fetch_array($result, null, PGSQL_ASSOC);
	print "<tr class='boxTab2'>";
	readTable();
	print "</tr>";
}
print "</table>";
pg_close($basisData);
?>
