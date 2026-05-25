document.getElementById('print_text').addEventListener('submit', async function (e) {
    e.preventDefault();
    const str = document.getElementById('input_text').value; const a = ".!?";
    let str2 = ""; let k = 0; let o = 0;
    for (let i = 0; i < str.length; i++) {
        let l = false; for (let j = 0; j < a.length; j++) { l ||= str[i] == a[j]; }
        if (l) k = 1; else if (k == 1 && o == 0) { str2 += "\n"; o = 1; }
        if ((str[i] != " " || k == 0) && str[i] != "\n") { str2 += str[i]; if (!l && k == 1) { k = 0; o = 0; } }
    }
    document.getElementById('output_text').value = str2;
});