import type { Todo, TodoCallback } from "./types.js";

export class TodoManage {
  private todos: Todo[] = [];
  private nextId: number = 1;

  addTodo(text: string): Todo {
    if (text.trim() === "") {
      throw new Error("Todo cannot be empty.");
    }
    const newTodo: Todo = {
      id: this.nextId++,
      text: text,
      completed: false,
      createdAt: new Date(),
    };
    this.todos.push(newTodo);
    return newTodo;
  }
  deleteTodo(id: number): boolean {
    const index = this.todos.findIndex((todo) => todo.id === id);
    if (index != -1) {
      this.todos.splice(index, 1);
      return true;
    }
    return false;
  }
  toggleTodo(id: number): Todo | null {
    const todo = this.todos.find((todo) => todo.id === id);
    if (todo) {
      todo.completed = !todo.completed;
      return todo;
    }
    return null;
  }
  getAllTodos(): Todo[] {
    return [...this.todos];
  }
  getTodoByStatus(completed: boolean): Todo[] {
    const todosByStatus = this.todos.filter(
      (todo) => todo.completed === completed
    );
    return todosByStatus;
  }
}
