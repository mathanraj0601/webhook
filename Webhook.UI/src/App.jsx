import { useState } from "react";
import "./App.css";

function App() {
  const [subscribeDto, setSubscribeDto] = useState({
    subscriberUrl: "",
    webHookType: "pricechange",
    publisher: "indcli",
  });
  const [option, setOption] = useState(0);
  const [sucess, setSucess] = useState(false);
  const [secretCode, setSecretCode] = useState("");

  const handleSubmit = () => {
    fetch("https://localhost:7101/api/Subscriber", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(subscribeDto),
    })
      .then(async (res) => {
        console.log();
        if (res.status == 500) throw new Error("Already exits");
        const data = await res.json();
        setSecretCode(data.secret);
        setSucess(true);
        reset();
      })
      .catch((ex) => {
        setSucess(false);
        alert("Not registered Please try again... " + ex?.message);
        reset();
      });
  };

  const reset = () => {
    setSubscribeDto({
      subscriberUrl: "",
      webHookType: "pricechange",
      publisher: "indcli",
    });
  };

  const onChangeUrl = (e) => {
    setSubscribeDto((prev) => ({ ...prev, subscriberUrl: e.target.value }));
  };

  const onSelect = (e) => {
    console.log(e.target.value);
    const webHooktype = options[e.target.value]?.label;
    setSubscribeDto((prev) => ({ ...prev, webHookType: webHooktype }));
    setOption(Number(e.target.value));
  };

  const options = [
    { id: 0, label: "pricechange" },
    { id: 1, label: "addnewclimate" },
  ];

  return (
    <div className="app-container">
      <div className="app">
        <div className="app-header">
          <h1> Climate WebHook Registration </h1>
        </div>
        <div className="app-body">
          {sucess && (
            <div className="success">
              sucessfully registered here is your secret code :{" "}
              <b>{secretCode}</b>
            </div>
          )}
          <h3>
            Publisher :
            <b>
              <u> INDCLI </u>
            </b>
          </h3>
          <div className="app-input">
            <div className="app-select">
              <label>Select Webhooktype</label>
              <select value={option} onChange={onSelect}>
                {options.map((option, index) => (
                  <option key={index} value={option.id}>
                    {option.label}
                  </option>
                ))}
              </select>
            </div>
            <div className="app-submit">
              <input
                onChange={onChangeUrl}
                type="text"
                placeholder="Enter POST URL"
                value={subscribeDto.subscriberUrl}
              />
              <button type="submit" onClick={handleSubmit}>
                Submit
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
