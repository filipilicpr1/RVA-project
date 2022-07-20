import { useEffect } from "react";
function App() {
  useEffect(() => {
    fetch("https://localhost:44386/api/users/login", {
      method: "POST",
      body: JSON.stringify({
        username: "CAO",
        password: "Zdravo",
      }),
      headers: {
        "Content-Type": "application/json",
      },
    }).then(res => res.json()
    ).then(data => console.log(data)).catch(err => alert(err.message));
  }, []);
  return (
    <div>
      <h1>Hello World!</h1>
    </div>
  );
}

export default App;
