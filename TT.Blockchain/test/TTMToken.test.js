
const { expect } = require("chai");
const { ethers } = require("hardhat");

describe("TTMToken", function () {

  it("deploie avec 10M TTM", async function () {
    const [owner] = await ethers.getSigners();
    const TTM = await ethers.getContractFactory("TTMToken");
    const supply = ethers.utils.parseUnits("10000000", 18);
    const ttm = await TTM.deploy(supply, owner.address);
    await ttm.deployed();

    const bal = await ttm.balanceOf(owner.address);
    expect(bal).to.equal(supply);
  });

  it("prend 1% de taxe sur transfert", async function () {
    const [owner, alice] = await ethers.getSigners();
    const TTM = await ethers.getContractFactory("TTMToken");
    const supply = ethers.utils.parseUnits("10000000", 18);
    const ttm = await TTM.deploy(supply, owner.address);
    await ttm.deployed();

    const montant = ethers.utils.parseUnits("1000", 18);
    await ttm.transfer(alice.address, montant);

    const envoi = ethers.utils.parseUnits("100", 18);
    const balAvant = await ttm.balanceOf(owner.address);
    await ttm.connect(alice).transfer(owner.address, envoi);
    const balApres = await ttm.balanceOf(owner.address);

    expect(balApres.gt(balAvant)).to.be.true;
  });

  it("bloque les transferts quand pause", async function () {
    const [owner, alice] = await ethers.getSigners();
    const TTM = await ethers.getContractFactory("TTMToken");
    const supply = ethers.utils.parseUnits("10000000", 18);
    const ttm = await TTM.deploy(supply, owner.address);
    await ttm.deployed();

    await ttm.pause();
    await expect(
      ttm.transfer(alice.address, ethers.utils.parseUnits("100", 18))
    ).to.be.revertedWith("TTM: token transfers are paused");
  });

});