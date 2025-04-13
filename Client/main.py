import httpx
from nicegui import ui

API_URL = "http://localhost:5282"

notes = []

async def load_notes():
    global notes
    async with httpx.AsyncClient() as client:
        resp = await client.get(f"{API_URL}/notes")
        notes = resp.json()
        notes_list.clear()
        for note in notes:
            with notes_list:
                ui.label(f"{note['title']}: {note['content']}")

async def create_note(title, content):
    async with httpx.AsyncClient() as client:
        await client.post(f"{API_URL}/notes", json={"title": title, "content": content})
    await load_notes()

ui.label("📓 Field Notes").classes("text-2xl font-bold")
ui.separator()

title = ui.input("Title").props("outlined")
content = ui.input("Content").props("outlined type=textarea")

ui.button("Add Note", on_click=lambda: create_note(title.value, content.value))

notes_list = ui.column()
ui.button("Reload Notes", on_click=load_notes)

ui.run(title="Field Notes UI", reload=False)

