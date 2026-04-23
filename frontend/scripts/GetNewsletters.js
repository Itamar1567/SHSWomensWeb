const backendUrl = "http://localhost:5100";
import { fileURLToPath } from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

import fs from "fs";
import path from "path";


async function GetNewsletters() {

    try{
        const res = await fetch(`${backendUrl}/api/newsletter`)
        const data = await res.json();
        if(!res.ok){
            throw new Error("Invalid request");
        }
        fs.writeFileSync(path.join(__dirname, "../public/newsletters.json"), JSON.stringify(data, null, 2));
        console.log("Writing to:", path.join(__dirname, "../public/newsletters.json"));
    }   
    catch(error){
        console.error("Failed to fetch newsletters", error);
    }
}    

GetNewsletters();
