"use client";
import { Button } from "@/components/ui/button";
import { listarUsuarios } from "@/services/AuthenticationService";
import { useEffect, useState } from "react";

export default function Home() {
  const Enviar = async () => {
    var resopnse = await listarUsuarios("c1ee5976-a394-4f36-26d3-08dcb0f79de5");
    console.log("Enviando");
  };

  return (
    <>
      <div>
        <Button onClick={Enviar}>Click me</Button>
      </div>
    </>
  );
}
