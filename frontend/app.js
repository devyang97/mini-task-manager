const API = 'http://localhost:5275/api/Tasks';

// Load all tasks when page opens
window.onload = loadTasks;

async function loadTasks() {
    const res = await fetch(API);
    const tasks = await res.json();

    const container = document.getElementById('tasks');

    if (tasks.length === 0) {
        container.innerHTML = '<p>No tasks yet.</p>';
        return;
    }

    container.innerHTML = tasks.map(task => `
        <div class="task-card ${task.isDone ? 'done' : ''}">
            <div class="task-info">
                <strong>${task.title}</strong>
                <span>${task.description || 'No description'}</span>
                <small>User ID: ${task.userId} | Created: ${new Date(task.createdAt).toLocaleDateString()}</small>
            </div>
            <div class="task-actions">
                <button onclick="toggleDone(${task.id}, ${task.isDone}, ${task.userId}, '${task.title}', '${task.description || ''}')">
                    ${task.isDone ? 'Undo' : 'Done'}
                </button>
                <button class="delete" onclick="deleteTask(${task.id})">Delete</button>
            </div>
        </div>
    `).join('');
}

async function createTask() {
    const title = document.getElementById('titleInput').value;
    const description = document.getElementById('descInput').value;
    const userId = parseInt(document.getElementById('userIdInput').value);

    if (!title || !userId) {
        alert('Please enter a title and user ID');
        return;
    }

    await fetch(API, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userId, title, description, isDone: false })
    });

    // Clear inputs
    document.getElementById('titleInput').value = '';
    document.getElementById('descInput').value = '';
    document.getElementById('userIdInput').value = '';

    loadTasks();
}

async function toggleDone(id, currentStatus, userId, title, description) {
    await fetch(`${API}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id, userId, title, description,
            isDone: !currentStatus
        })
    });
    loadTasks();
}

async function deleteTask(id) {
    if (!confirm('Delete this task?')) return;
    await fetch(`${API}/${id}`, { method: 'DELETE' });
    loadTasks();
}