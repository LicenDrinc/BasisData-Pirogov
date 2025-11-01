<?php
function readTable()
{
	global $line;
	foreach ($line as $col_value)
		print "<td>".$col_value."</td>";
}

function editingTable($f,$f1)
{
	if ($f == null)
		return '';
	else
		return $f1."='".$f."', ";
}

function returnExit($f)
{
	print '<p>'.$f.'</p>';
	exit;
}

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData) {
	print "<p>Произошла ошибка соединения: ";
	print pg_last_error(); exit;
}

$flag = 0;
if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$data = $_POST['idOrderEditing'];
	$table = $_POST['tableEditing'];
	
	if ($data == null)
		returnExit('id пуст');
	else
	{
		$query1 = 'SELECT * FROM '.$table;
		$result1 = pg_query($basisData, $query1);
		if (!$result1) {
			print "<p>Ошибка запроса: <p>";
			print pg_last_error();
			exit;
		}

		$p1 = pg_num_fields($result1); $z = 0;
		while ($line1 = pg_fetch_array($result1, null, PGSQL_ASSOC))
			if ($data == $line1['id'])
				$z += 1;
		if ($z == 0)
			returnExit('id невходит в диопозон');
	}
	
	if ($table == '_order')
	{
		$id_client = $_POST['id_clientEditing'];
		$id_worker = $_POST['id_workerEditing'];
		$date = $_POST['dateEditing'];
		$failure = $_POST['failureEditing'];
		$id_object = $_POST['id_objectEditing'];
		$price = $_POST['priceEditing'];
		$clientd_woeds = $_POST['clientd_woedsEditing'];
		$status = $_POST['statusEditing'];
		
		if ($id_client == null && $id_worker == null &&	$date == null &&
			$failure == null && $price == null && $clientd_woeds == null &&
			$status == null && $id_object == null)
			$flag = 1;
	}
	else if ($table == 'object')
	{
		$name = $_POST['nameEditing'];
		if ($name == null)
			$flag = 1;
	}
	else if ($table == 'client')
	{
		$id_human = $_POST['id_humanEditing'];
		if ($id_human == null)
			$flag = 1;
	}
	else if ($table == 'worker')
	{
		$id_human = $_POST['id_humanEditing'];
		if ($id_human == null)
			$flag = 1;
	}
	else if ($table == 'human')
	{
		$phone_number = $_POST['phone_numberEditing'];
		$name = $_POST['nameEditing'];
		if ($phone_number == null && $name == null)
			$flag = 1;
	}
}
else
{
	print "нет POST"; exit;
}

if ($flag == 0)
{
	$query = 'UPDATE '.$table.' SET ';

	if ($table == '_order')
	{
		$query .= editingTable($id_client,'id_client');
		$query .= editingTable($id_worker,'id_worker');
		$query .= editingTable($date,'date');
		$query .= editingTable($failure,'failure');
		$query .= editingTable($id_object,'id_object');
		$query .= editingTable($price,'price');
		$query .= editingTable($clients_words,'clients_words');
		$query .= editingTable($status,'status');
		$query .= editingTable($failure,'failure');
	}
	else if ($table == 'object')
	{
		$query .= editingTable($name,'name');
	}
	else if ($table == 'client')
	{
		$query .= editingTable($id_human,'id_human');
	}
	else if ($table == 'worker')
	{
		$query .= editingTable($id_human,'id_human');
	}
	else if ($table == 'human')
	{
		$query .= editingTable($name,'name');
		$query .= editingTable($phone_number,'phone_number');
	}

	if ($query[strlen($query)-2] == ',')
		$query = substr($query, 0, strlen($query)-2);

	$query .= ' WHERE id = '.$data.';';
}
else
{
	$query = 'DELETE FROM '.$table.' WHERE '.$data.' = id;';
}

//print '<p>'.$query;
$result = pg_query($basisData, $query);
if (!$result) {
	print "<p>Ошибка запроса: <p>";
	print pg_last_error();
	exit;
}

pg_close($con);
?>
