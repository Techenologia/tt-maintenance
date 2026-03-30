// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

// TTM Token - T&T Money
// Standard  : BEP-20 (compatible ERC-20)
// Supply max: 100,000,000 TTM
// Tax       : 1% per transfer to treasury
// Features  : Mint, Burn, Pause, Tax exempt
// Author    : T&T Dev Team

interface IBEP20 {
    function totalSupply() external view returns (uint256);
    function balanceOf(address account) external view returns (uint256);
    function transfer(address to, uint256 amount) external returns (bool);
    function allowance(address owner, address spender) external view returns (uint256);
    function approve(address spender, uint256 amount) external returns (bool);
    function transferFrom(address from, address to, uint256 amount) external returns (bool);

    event Transfer(address indexed from, address indexed to, uint256 value);
    event Approval(address indexed owner, address indexed spender, uint256 value);
}

contract TTMToken is IBEP20 {

    string  public constant name     = "T&T Money";
    string  public constant symbol   = "TTM";
    uint8   public constant decimals = 18;

    uint256 public constant MAX_SUPPLY = 100_000_000 * 10**18;
    uint256 private _totalSupply;

    uint256 public taxRate  = 100;
    address public treasury;

    address public owner;
    bool    public paused;

    mapping(address => uint256) private _balances;
    mapping(address => mapping(address => uint256)) private _allowances;
    mapping(address => bool) public taxExempt;

    uint256 public totalTaxCollected;
    uint256 public totalBurned;

    event Mint(address indexed to, uint256 amount);
    event Burn(address indexed from, uint256 amount);
    event TaxCollected(address indexed from, address indexed to, uint256 taxAmount);
    event TaxRateUpdated(uint256 oldRate, uint256 newRate);
    event TreasuryUpdated(address oldTreasury, address newTreasury);
    event Paused(address by);
    event Unpaused(address by);
    event OwnershipTransferred(address indexed previousOwner, address indexed newOwner);
    event TaxExemptUpdated(address indexed account, bool exempt);

    modifier onlyOwner() {
        require(msg.sender == owner, "TTM: caller is not the owner");
        _;
    }

    modifier whenNotPaused() {
        require(!paused, "TTM: token transfers are paused");
        _;
    }

    modifier validAddress(address addr) {
        require(addr != address(0), "TTM: zero address not allowed");
        _;
    }

    constructor(uint256 initialSupply, address _treasury) {
        require(initialSupply <= MAX_SUPPLY, "TTM: initial supply exceeds max");
        require(_treasury != address(0), "TTM: invalid treasury address");

        owner    = msg.sender;
        treasury = _treasury;

        taxExempt[msg.sender] = true;
        taxExempt[_treasury]  = true;

        _mint(msg.sender, initialSupply);
    }

    function totalSupply() external view override returns (uint256) {
        return _totalSupply;
    }

    function balanceOf(address account) external view override returns (uint256) {
        return _balances[account];
    }

    function allowance(address _owner, address spender) external view override returns (uint256) {
        return _allowances[_owner][spender];
    }

    function taxRatePercent() external view returns (uint256 numerator, uint256 denominator) {
        return (taxRate, 10_000);
    }

    function calculateTax(uint256 amount) public view returns (uint256) {
        return (amount * taxRate) / 10_000;
    }

    function transfer(address to, uint256 amount)
        external override whenNotPaused validAddress(to) returns (bool)
    {
        _transferWithTax(msg.sender, to, amount);
        return true;
    }

    function transferFrom(address from, address to, uint256 amount)
        external override whenNotPaused validAddress(to) returns (bool)
    {
        uint256 currentAllowance = _allowances[from][msg.sender];
        require(currentAllowance >= amount, "TTM: insufficient allowance");
        _allowances[from][msg.sender] = currentAllowance - amount;
        _transferWithTax(from, to, amount);
        return true;
    }

    function approve(address spender, uint256 amount)
        external override validAddress(spender) returns (bool)
    {
        _allowances[msg.sender][spender] = amount;
        emit Approval(msg.sender, spender, amount);
        return true;
    }

    function _transferWithTax(address from, address to, uint256 amount) internal {
        require(_balances[from] >= amount, "TTM: insufficient balance");

        uint256 taxAmount = 0;
        uint256 netAmount = amount;

        if (!taxExempt[from] && !taxExempt[to] && taxRate > 0) {
            taxAmount = calculateTax(amount);
            netAmount = amount - taxAmount;

            _balances[from]     -= taxAmount;
            _balances[treasury] += taxAmount;
            totalTaxCollected   += taxAmount;

            emit TaxCollected(from, to, taxAmount);
            emit Transfer(from, treasury, taxAmount);
        }

        _balances[from] -= netAmount;
        _balances[to]   += netAmount;

        emit Transfer(from, to, netAmount);
    }

    function _mint(address to, uint256 amount) internal {
        require(_totalSupply + amount <= MAX_SUPPLY, "TTM: max supply exceeded");
        _totalSupply  += amount;
        _balances[to] += amount;
        emit Transfer(address(0), to, amount);
        emit Mint(to, amount);
    }

    function _burn(address from, uint256 amount) internal {
        require(_balances[from] >= amount, "TTM: burn amount exceeds balance");
        _balances[from] -= amount;
        _totalSupply    -= amount;
        totalBurned     += amount;
        emit Transfer(from, address(0), amount);
        emit Burn(from, amount);
    }

    function mint(address to, uint256 amount) external onlyOwner validAddress(to) {
        _mint(to, amount);
    }

    function burn(uint256 amount) external {
        _burn(msg.sender, amount);
    }

    function burnFrom(address from, uint256 amount) external {
        uint256 currentAllowance = _allowances[from][msg.sender];
        require(currentAllowance >= amount, "TTM: insufficient allowance");
        _allowances[from][msg.sender] = currentAllowance - amount;
        _burn(from, amount);
    }

    function setTaxRate(uint256 newRate) external onlyOwner {
        require(newRate <= 500, "TTM: tax rate cannot exceed 5%");
        emit TaxRateUpdated(taxRate, newRate);
        taxRate = newRate;
    }

    function setTreasury(address newTreasury) external onlyOwner validAddress(newTreasury) {
        emit TreasuryUpdated(treasury, newTreasury);
        taxExempt[treasury]    = false;
        treasury               = newTreasury;
        taxExempt[newTreasury] = true;
    }

    function setTaxExempt(address account, bool exempt) external onlyOwner validAddress(account) {
        taxExempt[account] = exempt;
        emit TaxExemptUpdated(account, exempt);
    }

    function pause() external onlyOwner {
        require(!paused, "TTM: already paused");
        paused = true;
        emit Paused(msg.sender);
    }

    function unpause() external onlyOwner {
        require(paused, "TTM: not paused");
        paused = false;
        emit Unpaused(msg.sender);
    }

    function transferOwnership(address newOwner) external onlyOwner validAddress(newOwner) {
        taxExempt[owner]    = false;
        taxExempt[newOwner] = true;
        emit OwnershipTransferred(owner, newOwner);
        owner = newOwner;
    }

    function tokenStats() external view returns (
        uint256 supply,
        uint256 maxSupply,
        uint256 burned,
        uint256 taxCollected,
        uint256 currentTaxRate,
        bool    isPaused
    ) {
        return (
            _totalSupply,
            MAX_SUPPLY,
            totalBurned,
            totalTaxCollected,
            taxRate,
            paused
        );
    }
}
