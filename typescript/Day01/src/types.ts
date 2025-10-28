export interface Todo {
  id: number;
  text: string;
  completed: boolean;
  createdAt: Date;
}

export type TodoCallback = (todo: Todo) => void;
