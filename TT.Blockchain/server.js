const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const PORT = 5095;

// Middleware
app.use(cors());
app.use(bodyParser.json());

// Stockage en mémoire (pour test)
let users = [];         // { email, numero, nom, prenom, password, balance, matricule }
let transactions = [];  // { from, to, amount, date }

// ==================== Routes ====================

// Créer un compte
app.post('/api/wallet/create', (req, res) => {
    const { email, numero, nom, prenom, password } = req.body;
    if (users.find(u => u.numero === numero)) {
        return res.status(400).json({ message: "Numéro déjà utilisé" });
    }
    const user = { email, numero, nom, prenom, password, balance: 100, matricule: Math.floor(Math.random()*1000000) };
    users.push(user);
    res.json(user);
});

// Connexion
app.post('/api/wallet/login', (req, res) => {
    const { numero, password } = req.body;
    const user = users.find(u => u.numero === numero && u.password === password);
    if(!user) return res.status(401).json({ message: "Utilisateur introuvable ou mot de passe incorrect" });
    res.json(user);
});

// Transfert
app.post('/api/wallet/transfer', (req, res) => {
    const { fromNumero, toNumero, amount } = req.body;
    const sender = users.find(u => u.numero === fromNumero);
    const receiver = users.find(u => u.numero === toNumero);
    if(!sender || !receiver) return res.status(400).json({ success:false, message:"Numéro invalide" });
    if(sender.balance < amount) return res.status(400).json({ success:false, message:"Solde insuffisant" });
    
    sender.balance -= amount;
    receiver.balance += amount;
    
    transactions.push({ from: fromNumero, to: toNumero, amount, date: new Date() });
    res.json({ success:true });
});

// Historique
app.get('/api/wallet/ledger/:numero', (req, res) => {
    const numero = req.params.numero;
    const userTx = transactions.filter(t => t.from === numero || t.to === numero);
    res.json(userTx);
});

// Lancer serveur
app.listen(PORT, () => console.log(`TTM Backend running on port ${PORT}`));
