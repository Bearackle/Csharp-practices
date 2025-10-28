function greet(name: string): string {
  return `Hello, ${name}!`;
}

console.log(greet("TypeScript"));

const user = {
  name: "Daniel",
  age: 26,
};

user.name;

let a: string = "hello bigin";

console.log(a);

function printId(id: number | string) {
  console.log("Your ID is: " + id);
}

type UserString = string;

let str: UserString = "hello from universe";

console.log(str);

interface Point {
  x: number;
  y: number;
}
declare function handleRequest(url: string, method: "GET" | "POST"): void;

const req = { url: "https://example.com", method: "POST" };

// handleRequest(req.url, req.method as "GET");

function greeter(fn: (a: string) => void) {
  fn("Hello, World");
}

function printToConsole(s: string) {
  console.log(s);
}

greeter(printToConsole);
i