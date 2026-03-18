import mysql.connector
from datetime import datetime

try:
    conn = mysql.connector.connect(
        host="127.0.0.1",
        port=3306,
        database="taskmanager_db",
        user="root",
        password="rootroot"
    )
    cursor = conn.cursor()

    print("=" * 40)
    print("  TASK MANAGER REPORT")
    print(f"  Generated: {datetime.now().strftime('%Y-%m-%d %H:%M')}")
    print("=" * 40)

    cursor.execute("SELECT COUNT(*) FROM Tasks")
    total = cursor.fetchone()[0]
    print(f"\nTotal tasks:     {total}")

    cursor.execute("SELECT COUNT(*) FROM Tasks WHERE is_done = 1")
    done = cursor.fetchone()[0]
    print(f"Completed:       {done}")
    print(f"Pending:         {total - done}")

    print("\nTasks per user:")
    cursor.execute("""
        SELECT u.name, COUNT(t.id) AS total
        FROM Users u
        LEFT JOIN Tasks t ON u.id = t.user_id
        GROUP BY u.name
        ORDER BY total DESC
    """)
    for row in cursor.fetchall():
        print(f"  {row[0]}: {row[1]} tasks")

    print("\n" + "=" * 40)
    cursor.close()
    conn.close()

except Exception as e:
    print(f"Error: {e}")