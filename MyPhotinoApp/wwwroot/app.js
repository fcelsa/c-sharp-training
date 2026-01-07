function sendMessage(data) {
    window.external.sendMessage(JSON.stringify(data));
}

function handleOK() {
    const status = document.getElementById('test-status');
    status.innerHTML = '✓ Button OK cliccato - File JS caricato correttamente!';
}

function handleClose() {
    sendMessage({ command: 'log', data: 'Chiusura applicazione...' });
    // La chiusura reale avverrà dal backend
}

function openWindow() {
    sendMessage({ command: 'openWindow', data: 'Nuova Finestra ' + Date.now() });
}

function openModal(title) {
    sendMessage({ command: 'openModal', data: title || 'Dialog Modal' });
}

function showDebug() {
    sendMessage({ command: 'showDebugConsole' });
}

function executeCommand(cmd) {
    if (!cmd) {
        cmd = prompt('Inserisci comando:');
    }
    if (cmd) {
        sendMessage({ command: 'executeCommand', data: cmd });
    }
}

function clearOutput() {
    document.getElementById('test-status').innerHTML = '';
}

function commandResult(result) {
    document.getElementById('test-status').innerHTML = '<strong>Risposta dal C#:</strong><br>' + result;
}

// Log di avvio
sendMessage({ command: 'log', data: 'Applicazione WebView avviata' });
