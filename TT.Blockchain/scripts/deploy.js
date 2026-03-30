const { ethers } = require("hardhat");
require("dotenv").config();

async function main() {
  const [deployer] = await ethers.getSigners();
  console.log("Deploiement par :", deployer.address);

  const balance = await ethers.provider.getBalance(deployer.address);
  console.log("Balance :", balance.toString(), "wei");

  const treasury = deployer.address;
  const initialSupply = ethers.utils.parseUnits("10000000", 18);

  const TTM = await ethers.getContractFactory("TTMToken");
  const ttm = await TTM.deploy(initialSupply, treasury);
  await ttm.deployed();

  console.log("Contrat TTM deploye a :", ttm.address);
  console.log("Supply initiale : 10,000,000 TTM");
}

main().catch((e) => { console.error(e); process.exit(1); });