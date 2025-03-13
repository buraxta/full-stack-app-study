"use client";

import { Button } from "@/components/ui/button";
import { db } from "@/db";
import { useEffect, useState } from "react";
type TodoType = {
  id: string;
  title: string;
  isCompleted: boolean;
  description: string;
};
export default function Home() {
  const [item, setItem] = useState<TodoType[]>([]);
  const [newItem, setNewItem] = useState<string>("");

  useEffect(() => {
    const fetchData = async () => {
      const response = await db.get("");
      setItem(response.data);
    };
    fetchData();
  }, []);

  return (
    <div className="mx-auto w-[50%]  mt-[10%] p-4 flex flex-col items-center">
      <div>
        <div className="">
          <input
            type="text"
            className="border p-1 mr-5"
            value={newItem}
            onChange={(e) => setNewItem(e.target.value)}
          />
          <Button
            className="bg-blue-600 text-white"
            variant="default"
            onClick={async () => {
              if (!newItem) return;
              await db.post("", { title: newItem });
              setNewItem("");
              const response = await db.get("");
              setItem(response.data);
            }}
          >
            Add
          </Button>
        </div>
        <ul className="flex flex-col gap-2 mt-5 ">
          {item.map((item) => (
            <div
              key={item.id}
              className="flex gap-10 items-center justify-between "
            >
              <li>{item.title}</li>
              <Button
                className="bg-red-500 py-1 px-3"
                onClick={async () => {
                  await db.delete(item.id);
                  const response = await db.get("");
                  setItem(response.data);
                }}
              >
                Delete
              </Button>
            </div>
          ))}
        </ul>
      </div>
    </div>
  );
}
