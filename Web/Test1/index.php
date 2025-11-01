<?php
print '<HTML>';
print '<HEAD>';
print '<meta http-equiv="Content-Type" content="text/html; charset=utf-8">';
print '<TITLE>ТАРАНИН</TITLE>';
print '<meta name="author" content="Unknown" >';
print '<meta name="date" content="2025-03-10T14:08:56+0500" >';
print '</HEAD>';
print '<BODY>';
print '<table border="1" rules="all" width="100%">';
print '<caption>test</caption>';
print '<tbody>';
$x = 25; $y = 10;
for ($i = 1; $i < $y; $i++)
{
	print '<tr>';
	for ($j = 1; $j < $x; $j++)
	{
		print '<td>'; print($i-1)*$x+$j; print'</td>';
	}
	print '</tr>';
}

print '</tbody></table>';
print '</body>';
print '</html>';
?>
