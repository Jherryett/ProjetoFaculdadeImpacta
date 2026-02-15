const form = document.getElementById("formRoupa");

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const roupa = {
        nomeRoupa: document.getElementById("nomeRoupa").value,
        nomeFabricante: document.getElementById("nomeFabricante").value,
        valorPeca: document.getElementById("valorPeca").value,
        quantidadeEstoque: document.getElementById("quantidadeEstoque").value,
        observacoes: document.getElementById("observacoes").value
    };

    try {
        const response = await fetch("https://localhost:7179/api/roupas", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(roupa)
        });

        if (!response.ok) {
            throw new Error("Erro ao cadastrar roupa");
        }

        alert("Roupa cadastrada com sucesso!");
        form.reset();

    } catch (error) {
        console.error(error);
        alert("Erro ao enviar informação para a API");
    }
});