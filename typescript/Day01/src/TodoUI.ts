import type { Todo, TodoCallback } from "./types.js";

export class TodoUI {
  private listElement: HTMLUListElement;
  private onDelete: TodoCallback;
  private onToggle: TodoCallback;

  constructor(
    listElementId: string,
    onDelete: TodoCallback,
    onToggle: TodoCallback
  ) {
    const element = document.getElementById(listElementId);
    if (!element || !(element instanceof HTMLUListElement)) {
      throw new Error("Invalid list element");
    }
    this.listElement = element;
    this.onDelete = onDelete;
    this.onToggle = onToggle;
  }
  render(todos: Todo[]): void {
    this.listElement.innerHTML = "";
    if (todos.length === 0) {
      this.listElement.innerHTML =
        '<li class="empty"> No todos available.</li>';
      return;
    }

    todos.forEach((todo) => {
      const i = this.createTodoElement(todo);
      this.listElement.appendChild(i);
    });
  }
  createTodoElement(todo: Todo): HTMLLIElement {
    const li = document.createElement("li");
    li.className = todo.completed ? "completed" : "";

    const checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.checked = todo.completed;
    checkbox.addEventListener("change", () => this.onToggle(todo));
    const span = document.createElement("span");
    span.textContent = todo.text;

    const deleteBtn = document.createElement("button");
    deleteBtn.textContent = "Delete";
    deleteBtn.className = "delete-btn";
    deleteBtn.addEventListener("click", () => this.onDelete(todo));
    li.appendChild(checkbox);
    li.appendChild(span);
    li.appendChild(deleteBtn);
    return li;
  }
}
