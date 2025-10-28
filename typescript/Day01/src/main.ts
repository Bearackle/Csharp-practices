import { TodoManage } from "./TodoManage.js";
import { TodoUI } from "./TodoUI.js";
import type { Todo } from "./types.js";

const todoManage = new TodoManage();

const todoUI = new TodoUI(
  "todoList",
  (todo: Todo) => handleDelete(todo),
  (todo: Todo) => handleToggle(todo)
);
const inputElement = document.getElementById("todoInput") as HTMLInputElement;
const addBtn = document.getElementById("addBtn") as HTMLButtonElement;

function handleAdd(): void {
  try {
    const text = inputElement.value;
    todoManage.addTodo(text);
    inputElement.value = "";
    UpdateUI();
  } catch (error) {
    if (error instanceof Error) {
      alert(error.message);
    }
  }
}
function handleDelete(todo: Todo): void {
  if (confirm(`Are you sure you want to delete the todo: "${todo.text}"?`)) {
    todoManage.deleteTodo(todo.id);
    UpdateUI();
  }
}
function handleToggle(todo: Todo): void {
  todoManage.toggleTodo(todo.id);
  UpdateUI();
}
function UpdateUI(): void {
  const todos = todoManage.getAllTodos();
  todoUI.render(todos);
}

addBtn.addEventListener("click", () => handleAdd());
inputElement.addEventListener("keypress", (event) => {
  if (event.key === "Enter") {
    handleAdd();
  }
});

UpdateUI();
